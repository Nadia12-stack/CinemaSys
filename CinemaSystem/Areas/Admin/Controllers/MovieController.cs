using CinemaSystem.Models;
using CinemaSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace CinemaSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        ApplicationDbContext _context = new();

        Repository<Cinema> _cinemaRepository = new();
        Repository<Actor> _actorRepository = new();
        MovieRepository _movieRepository = new();
        Repository<Category> _categoryRepository = new();
        Repository<MovieActor> _movieActorRepository = new();
        Repository<MovieCinema> _movieCinemaRepository = new();
        Repository<MovieSubImage> _movieSubImageRepository = new();

        DictionaryRepository<Cinema> _cinemaDictionaryRepository = new();
        DictionaryRepository<Actor> _actorDictionaryRepository = new();



        public async Task<IActionResult> Index(FilterMovieVM filterMovieVM, CancellationToken cancellationToken, int page = 1)
        {
            const int pageSize = 8;

           
            var movies = await _movieRepository.GetAsync(
                includes: [m => m.Category, m => m.MovieCinemas, m => m.MovieActors],
                tracked: false,
                cancellationToken: cancellationToken
            );

            #region Apply Filters

            if (!string.IsNullOrWhiteSpace(filterMovieVM.name))
                movies = movies.Where(m => m.Name.Contains(filterMovieVM.name.Trim(), StringComparison.OrdinalIgnoreCase));

            if (filterMovieVM.categoryId.HasValue)
                movies = movies.Where(m => m.CategoryId == filterMovieVM.categoryId.Value);

            if (filterMovieVM.cinemaId.HasValue)
                movies = movies.Where(m => m.MovieCinemas.Any(mc => mc.CinemaId == filterMovieVM.cinemaId.Value));

            #endregion

            #region Load Related Cinemas and Actors

            var cinemas = await _cinemaRepository.GetAsync();
            var actors = await _actorRepository.GetAsync();

            var cinemaDict = _cinemaDictionaryRepository.ToIdDictionary(cinemas);
            var actorDict = _actorDictionaryRepository.ToIdDictionary(actors);

            foreach (var movie in movies)
            {
                foreach (var mc in movie.MovieCinemas)
                {
                    if (cinemaDict.TryGetValue(mc.CinemaId, out var cinema))
                        mc.Cinema = cinema;
                }

                foreach (var ma in movie.MovieActors)
                {
                    if (actorDict.TryGetValue(ma.ActorId, out var actor))
                        ma.Actor = actor;
                }
            }

            #endregion

            #region Pagination

            var totalMovies = movies.Count();
            var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            var pagedMovies = movies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            #endregion

            #region ViewBag Data

            ViewBag.Filter = filterMovieVM;
            ViewBag.TotalMovies = totalMovies;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAsync(), "Id", "Name");
            ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");

            #endregion

            return View(pagedMovies);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAsync();
            var cinemas =await _cinemaRepository.GetAsync();
            var actors = await _actorRepository.GetAsync();

            return View(new MovieVM
            {
                Categories = categories.AsEnumerable(),
                Cinemas = cinemas.AsEnumerable(),
                actors = actors.AsEnumerable()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movieVM, IFormFile img, List<IFormFile>? subImgs, string[] actors, List<int> cinemaIds,CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                var movie = movieVM.Movie!;

                // Main Image
                if (img is not null && img.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);
                    using var stream = System.IO.File.Create(filePath);
                    img.CopyTo(stream);
                    movie.MainImg = fileName;
                }

                // Save Movie
                await _movieRepository.AddAsync(movie,cancellationToken);
              await  _movieRepository.CommitAsync(cancellationToken);


                // Sub Images
                if (subImgs is not null)
                {
                    foreach (var item in subImgs)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(item.FileName);
                        var filePath = Path.Combine("wwwroot/images/movieimages", fileName);
                        using var stream = System.IO.File.Create(filePath);
                        item.CopyTo(stream);

                        await _movieSubImageRepository.AddAsync(new MovieSubImage
                        {
                            Img = fileName,
                            MovieId = movie.Id
                        },cancellationToken);
                    }
                    await _movieSubImageRepository.CommitAsync(cancellationToken);
                }

                // Save Actors
                if (actors is not null)
                {
                    foreach (var actorId in actors)
                    {
                      await _movieActorRepository.AddAsync(new MovieActor
                        {
                            ActorId = int.Parse(actorId),
                            MovieId = movie.Id
                        },cancellationToken);
                    }
                   await _movieActorRepository.CommitAsync(cancellationToken);
                }

                // Save Cinemas
                if (cinemaIds is not null)
                {
                    foreach (var cinemaId in cinemaIds)
                    {
                      await  _movieCinemaRepository.AddAsync(new MovieCinema
                        {
                            CinemaId = cinemaId,
                            MovieId = movie.Id
                        },cancellationToken);
                    }
                  await  _movieCinemaRepository.CommitAsync(cancellationToken);
                }

                TempData["SuccessMessage"] = "Add Movie Successfully";
                transaction.Commit();
            }
            catch
            {
                TempData["error-notification"] = "Error While Saving the movie";
                transaction.Rollback();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
          
            var movie =await _movieRepository.GetOneAsync( m => m.Id == id,
                includes:[m => m.MovieActors, m => m.MovieCinemas, m => m.Category]
                ,tracked:false);

            if (movie == null)
                return NotFound();

            var subImages =await _movieSubImageRepository.GetAsync();
            subImages.Where(s => s.MovieId == id).ToList();

            var categories = await _categoryRepository.GetAsync();
            var cinemas = await _cinemaRepository.GetAsync();
            var actors = await _actorRepository.GetAsync();
            var movieVM = new MovieVM
            {
                Movie = movie,
                MovieSubImages = subImages,

                Categories = categories.AsEnumerable(),
                Cinemas = cinemas.AsEnumerable(),
                actors = actors.AsEnumerable()
            };

            return View(movieVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, IFormFile? img, List<IFormFile>? subImgs, string[] actors, List<int> cinemaIds)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                var movieInDb = await _movieRepository.GetOneAsync(e => e.Id == movie.Id,
               includes: [m => m.MovieActors, m => m.MovieCinemas],tracked:false
               );
               

                if (movieInDb is null)
                    return RedirectToAction("NotFoundPage", "Home");

                // Main Image
                if (img is not null && img.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);
                    using var stream = System.IO.File.Create(filePath);
                    img.CopyTo(stream);

                    var oldPath = Path.Combine("wwwroot/images", movieInDb.MainImg);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);

                    movie.MainImg = fileName;
                }
                else
                {
                    movie.MainImg = movieInDb.MainImg;
                }

                _movieRepository.Update(movie);
               await _movieRepository.CommitAsync();

                // Sub Images
                if (subImgs is not null && subImgs.Count > 0)
                {
                    foreach (var item in subImgs)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(item.FileName);
                        var filePath = Path.Combine("wwwroot/images/movieimages", fileName);
                        using var stream = System.IO.File.Create(filePath);
                        item.CopyTo(stream);

                     await   _movieSubImageRepository.AddAsync(new MovieSubImage
                        {
                            Img = fileName,
                            MovieId = movie.Id
                        });
                    }
                   await _movieSubImageRepository.CommitAsync();
                }

                // Update Actors
                var oldActors = await _movieActorRepository.GetAsync(a => a.MovieId == movie.Id);
                _movieActorRepository.Delete(oldActors);
                await _movieActorRepository.CommitAsync();

                if (actors is not null)
                {
                    foreach (var actorId in actors)
                    {
                        await _movieActorRepository.AddAsync(new MovieActor
                        {
                            ActorId = int.Parse(actorId),
                            MovieId = movie.Id
                        });
                    }
                   await _movieActorRepository.CommitAsync();
                }

                // Update Cinemas
                var oldCinemas = await _movieCinemaRepository.GetAsync(c => c.MovieId == movie.Id);
                _movieCinemaRepository.Delete(oldCinemas);
                await _movieCinemaRepository.CommitAsync();

                if (cinemaIds is not null)
                {
                    foreach (var cinemaId in cinemaIds)
                    {
                       await _movieCinemaRepository.AddAsync(new MovieCinema
                        {
                            CinemaId = cinemaId,
                            MovieId = movie.Id
                        });
                    }
                 await  _movieCinemaRepository.CommitAsync();
                }

                TempData["SuccessMessage"] = "Update Movie Successfully";
                transaction.Commit();
            }
            catch
            {
                TempData["error-notification"] = "Error While Updating the movie";
                transaction.Rollback();
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetOneAsync(e => e.Id == id, includes: [e => e.movieSubImages]);

            if (movie is null)
                return RedirectToAction("NotFoundPage", "Home");

            var oldPath = Path.Combine("wwwroot/images", movie.MainImg);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);

            foreach (var item in movie.movieSubImages)
            {
                var subImgOldPath = Path.Combine("wwwroot/images/movieimages", item.Img);
                if (System.IO.File.Exists(subImgOldPath))
                    System.IO.File.Delete(subImgOldPath);
            }

            _movieRepository.Delete(movie);
            await _movieRepository.CommitAsync();

            TempData["SuccessMessage"] = "Delete Movie Successfully";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteSubImg(int movieId, string Img)
        {
            var movieSubImgInDb = await _movieSubImageRepository.GetOneAsync(e => e.MovieId == movieId && e.Img == Img);

            if (movieSubImgInDb is null)
                return RedirectToAction("NotFoundPage", "Home");

            var oldPath = Path.Combine("wwwroot/images/movieimages", movieSubImgInDb.Img);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);

            _movieSubImageRepository.Delete(movieSubImgInDb);
            await _movieSubImageRepository.CommitAsync();

            return RedirectToAction(nameof(Edit), new { id = movieId });
        }



        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieRepository.GetOneAsync(
                expression: m => m.Id == id,
                includes: [m => m.Category, m => m.MovieActors, m => m.MovieCinemas, m => m.movieSubImages],
                tracked: false
            );

            if (movie == null)
                return NotFound();

            var cinemas = await _cinemaRepository.GetAsync();
            var actors = await _actorRepository.GetAsync();

            var cinemaDict = _cinemaDictionaryRepository.ToIdDictionary(cinemas);
            var actorDict = _actorDictionaryRepository.ToIdDictionary(actors);

            foreach (var mc in movie.MovieCinemas)
            {
                if (cinemaDict.TryGetValue(mc.CinemaId, out var cinema))
                    mc.Cinema = cinema;
            }

            foreach (var ma in movie.MovieActors)
            {
                if (actorDict.TryGetValue(ma.ActorId, out var actor))
                    ma.Actor = actor;
            }

            var viewModel = new MovieVM
            {
                Movie = movie,
                MovieSubImages = movie.movieSubImages
            };

            return View(viewModel);
        }


    }
}
