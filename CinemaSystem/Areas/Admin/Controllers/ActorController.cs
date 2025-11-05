using CinemaSystem.Models;
using CinemaSystem.Utitlies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE},{SD.EMPLOYEE_ROLE}")]

    public class ActorController : Controller
    {
        //private readonly ApplicationDbContext _context=new();
        private readonly Repository<Actor> _actorRepository = new();
        private readonly Repository<Movie> _movieRepository = new();
        private readonly Repository<SocialLink> _socialLinkRepository = new();


        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 11;

            var actors = await _actorRepository.GetAsync(
            includes: [a => a.MovieActors, a => a.SocialLinks],
            tracked: false
        );
            var totalActors = actors.Count();
            var pagedActors = actors
                .OrderBy(a => a.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalActors / pageSize);
            ViewBag.TotalActors = totalActors;

            return View(pagedActors);
        
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Actor());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actor actor, IFormFile? img)
        {

            if (img is not null && img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/actors", fileName);
                using var stream = System.IO.File.Create(path);
                img.CopyTo(stream);
                actor.Img = fileName;
            }

            await _actorRepository.AddAsync(actor);
            await _actorRepository.CommitAsync();
            TempData["SuccessMessage"] = "Actor has been added successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorRepository.GetOneAsync(a => a.Id == id);
            if (actor is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(actor);
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]

        public async Task<IActionResult> Edit(Actor actor, IFormFile? img)
        {
            var actorInDb =await _actorRepository.GetOneAsync(a => a.Id == actor.Id, tracked: false);
            if (actorInDb is null)
                return RedirectToAction("NotFoundPage", "Home");



            if (img is not null && img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/actors", fileName);
                using var stream = System.IO.File.Create(path);
                img.CopyTo(stream);

                var oldPath = Path.Combine("wwwroot/images/actors", actorInDb.Img);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                actor.Img = fileName;
            }
            else
            {
                actor.Img = actorInDb.Img;
            }

            _actorRepository.Update(actor);
            await _actorRepository.CommitAsync();

            TempData["SuccessMessage"] = "Actor has been updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]

        public async Task<IActionResult> Delete(int id)
        {
            var actor =await _actorRepository.GetOneAsync(a => a.Id == id);
            if (actor is null)
                return RedirectToAction("NotFoundPage", "Home");

            var path = Path.Combine("wwwroot/images/actors", actor.Img);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _actorRepository.Delete(actor);
            await _actorRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _actorRepository.GetOneAsync(
            a => a.Id == id,
            includes: [a => a.MovieActors, a => a.SocialLinks]
        );
            if (actor == null)
                return NotFound();

            return View(actor);
        }


    }
}
