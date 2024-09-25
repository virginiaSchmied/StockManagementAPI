﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockManagementAPI.Models.Product
{
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required(ErrorMessage = "The field ProductName is required")]
        public string ProductName { get; set; }
    }
}
