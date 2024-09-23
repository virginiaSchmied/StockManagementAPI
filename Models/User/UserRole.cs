using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockManagementAPI.Models.User
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
        public int Id { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "The field Description is required")]
        public string Description { get; set; }
    }
}
