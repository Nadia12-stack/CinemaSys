
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaSystem.ViewModels
{
    public class MovieVM
    {
        public Movie? Movie { get; set; }
        public IEnumerable<Category> Categories { get; set; } = null!;
        public IEnumerable<Cinema> Cinemas { get; set; } = null!;
        public IEnumerable<Entity> entity { get; set; } = null!;
        public IEnumerable<Actor> actors { get; set; } = null!;
        public IEnumerable<MovieCinema> MovieCinemas { get; set; } = null!;
        public IEnumerable<SocialLink> socialLinks { get; set; } = null!;
        public IEnumerable<MovieActor> MovieActors { get; set; } = null!;
        public IEnumerable<MovieCinema> movieCinemas { get; set; } = null!;
        public IEnumerable<MovieSubImage> MovieSubImages { get; set; } = null!;
        public List<Actor>? Actor { get; internal set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; } = null!;

    }
}
