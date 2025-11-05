using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.ViewModels
{
    public class UpdateCinemaVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Description { get; set; }
        public bool Status { get; set; }
        public string? Img { get; set; }
        public IFormFile? NewImg { get; set; }
    }
}
