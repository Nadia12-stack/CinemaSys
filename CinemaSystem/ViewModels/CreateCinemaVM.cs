using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace CinemaSystem.ViewModels
{
    public class CreateCinemaVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Description { get; set; }
        public bool Status { get; set; }
        public IFormFile Img { get; set; } = default!;
    }
}
