using CinemaSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Threading.Tasks;

namespace CinemaSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        //private  ApplicationDbContext _context=new();

        Repository<Cinema> _cinemaRepository = new();

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var cinemas = await _cinemaRepository.GetAsync(tracked: false,cancellationToken:cancellationToken);

            return View(cinemas.AsEnumerable());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCinemaVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCinemaVM vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var cinema = vm.Adapt< Cinema>();

            if (vm.Img is not null && vm.Img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(vm.Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using var stream = System.IO.File.Create(filePath);
                vm.Img.CopyTo(stream);

                cinema.Img = fileName;
            }

            await _cinemaRepository.AddAsync(cinema,cancellationToken);
            await _cinemaRepository.CommitAsync(cancellationToken);

            TempData["success-notification"] = "Cinema added successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var cinema = await _cinemaRepository.GetOneAsync(e => e.Id == id,cancellationToken:cancellationToken);
            if (cinema is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(cinema.Adapt<UpdateCinemaVM>());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCinemaVM updateCinemaVM, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(updateCinemaVM);
            var cinemaInDb = await _cinemaRepository.GetOneAsync(e => e.Id == updateCinemaVM.Id, tracked: false,cancellationToken:cancellationToken);

            // = _context.Cinemas.AsNoTracking().FirstOrDefault();
            if (cinemaInDb is null)
                return RedirectToAction("NotFoundPage", "Home");

            var cinema = updateCinemaVM.Adapt<Cinema>();

            if (updateCinemaVM.NewImg is not null && updateCinemaVM.NewImg.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(updateCinemaVM.NewImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using var stream = System.IO.File.Create(filePath);
                updateCinemaVM.NewImg.CopyTo(stream);

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", cinemaInDb.Img);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                cinema.Img = fileName;
            }
            else
            {
                cinema.Img = cinemaInDb.Img;
            }

            _cinemaRepository.Update(cinema);
            await _cinemaRepository.CommitAsync(cancellationToken);

            TempData["success-notification"] = "Cinema updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var cinema = await _cinemaRepository.GetOneAsync(e => e.Id == id,cancellationToken:cancellationToken);
            if (cinema is null)
                return RedirectToAction("NotFoundPage", "Home");

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", cinema.Img);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);

            _cinemaRepository.Delete(cinema);
            await _cinemaRepository.CommitAsync(cancellationToken);

            TempData["success-notification"] = "Cinema deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
