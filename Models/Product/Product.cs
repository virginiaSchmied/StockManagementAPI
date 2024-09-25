using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementAPI.Models.Product
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime UploadDate { get; set; }

        [ForeignKey("ProductCategory_Id")]
        public int IdProductCategory_Id { get; set; }
    }
}
