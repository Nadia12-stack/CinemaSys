using CinemaSystem.Validations;
using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{      
   

    public class Category
    {
        public int Id { get; set; }
        [Required]
        [CustomLength(4, 100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Description { get; set; }
        public bool Status { get; set; }

        //public List<Movie> Movies { get; set; } = new();

    }
}
