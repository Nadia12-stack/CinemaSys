using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Models
{
   
    public class MovieSubImage
    {
        public int MovieId { get; set; }
        public Movie Movie { get; } = null!;
        public string Img { get; set; } = string.Empty;
    }
}
