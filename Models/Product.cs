using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Name is required and must be 2–50 characters
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty; //avoids null warnings and keeps Name non-null
    }

}
