using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Name is required and must be 2–50 characters
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; } = string.Empty; //avoids null warnings and keeps Name non-null
    }

}
