namespace CinemaSystem.ViewModels
{
    public class MovieRelatedVM
    {
        public Movie movie { get; set; } = default!;

        public List<Movie> Relatedmovies { get; set; } = [];
    }
}
