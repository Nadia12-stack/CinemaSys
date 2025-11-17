using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Movie name is required.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
        public bool Status { get; set; }
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10,000.")]
        public decimal Price { get; set; }
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public decimal Discount { get; set; }//On Ticket
        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5.")]
        public double?Rate { get; set; }
        public long Traffic { get; set; }

        public string MainImg { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Category selection is required.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public List<MovieActor> MovieActors { get; set; } = new();

        
        public List<MovieSubImage> movieSubImages { get; set; } = null!;
        public List<MovieCinema> MovieCinemas { get; set; } = new();

    }
}
