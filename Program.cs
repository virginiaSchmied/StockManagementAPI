using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.User;
using StockManagementAPI.Repositories.Interfaces;
using StockManagementAPI.Repositories;
using StockManagementAPI.Services;
using System.Globalization;
using System.Text;
using StockManagementAPI.Models.Product;

namespace StockManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

            // Add services to the container.
            builder.Configuration.AddJsonFile("appsettings.json");
            var secretKey = builder.Configuration.GetSection("settings:secretkey").Value;
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RegularPolicy", policy => policy.RequireRole("User"));
            });


            builder.Services.AddDbContext<stockManagementDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection"), sqlOptions =>
                {
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    sqlOptions.EnableRetryOnFailure();
                })
            );


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockManagement", Version = "v1" });

                // Swagger Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddScoped<User>();
            builder.Services.AddScoped<UserRole>();
            builder.Services.AddScoped<Product>();
            builder.Services.AddScoped<ProductCategory>();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserRoleService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductCategoryService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Add UseRouting before UseEndpoints
            app.UseRouting();


            // Middleware for 401 and 403 Status Code
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = 401,
                        message = "Authentication is required to access this resource."
                    }));
                }

                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = 403,
                        message = "Access denied."
                    }));
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}

