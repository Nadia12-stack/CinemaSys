using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var activeActors = _context.Actors.Count(a => a.Status);
            var inactiveActors = _context.Actors.Count(a => !a.Status);
            var activeMovies = _context.Movies.Count(m => m.Status);
            var inactiveMovies = _context.Movies.Count(m => !m.Status);

            var totalCinemas = _context.Cinemas.Count();

            var topCategory = _context.Movies
                .GroupBy(m => m.Category.Name)
                .OrderByDescending(g => g.Count())
                .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                .FirstOrDefault();

            ViewBag.ActiveActors = activeActors;
            ViewBag.InactiveActors = inactiveActors;
            ViewBag.ActiveMovies = activeMovies;
            ViewBag.InactiveMovies = inactiveMovies;
            ViewBag.TopCategory = topCategory?.CategoryName ?? "N/A";

            ViewBag.TotalCinemas = totalCinemas;
            ViewBag.TotalCategories = _context.Categories.Count();
            ViewBag.TotalActors = _context.Actors.Count();
            ViewBag.TotalMovies = _context.Movies.Count();

            return View();
        }
    }

}
