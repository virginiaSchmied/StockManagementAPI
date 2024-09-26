## Stock Management ASP.NET WEB API

Esta aplicacion contiene los endpoints del sistema de gestion de stock de productos Stock Management.


## Instalación

## Repositorio a clonar: https://github.com/virginiaSchmied/StockManagementAPI.git. 
La solución se encuentra en la rama dev. Realizar el cambio a dicha rama a través de git checkout dev.


 
## Conexion a SQL
Antes de ejecutar los comandos para crear la base de datos y realizar la migración de datos, en el archivo appsettings.json corroborar y modificar de ser necesario el nombre de la instancia de SQL Server Express (localhost\\SQLEXPRESS).

"ConnectionStrings": 
{"MyDbConnection":"Server=localhost\\SQLEXPRESS;Database=stockManagement; Persist Security Info=True;Integrated
Security=True; Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true;"},



## Creacion de base de datos y migracion de datos 
En PowerShell para desarrolladores, ejecutar los siguientes comandos:

dotnet ef migrations add InitialCreate

dotnet ef database update

Estos comandos crean la base de datos de nombre stockManagement y completa las tablas con la informacion
especificada en el seed destinado a cada entidad.





Corroborar que la base de datos se haya creado exitosamente. Luego, ejecutar la aplicacion.



