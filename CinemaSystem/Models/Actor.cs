using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace CinemaSystem.Models
{

    public class Actor : Entity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [CustomLength(3, 100)]
        public string Name { get; set; } = string.Empty;

        [CustomLength(0, 500)]
        public string? Description { get; set; }
        [CustomLength(0, 300)]
        public string? Awards { get; set; }
        public bool Status { get; set; }
        public string Img { get; set; } = "defaultImg.png";
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [CustomLength(2, 50)] 
        public string Nationality { get; set; } = string.Empty;

        public List<MovieActor> MovieActors { get; set; } = new();

        public List<SocialLink> SocialLinks { get; set; } = new();

    }
}
