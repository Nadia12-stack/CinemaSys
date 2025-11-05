namespace CinemaSystem.Models
{
    public class MovieCinema
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;
    }
}
