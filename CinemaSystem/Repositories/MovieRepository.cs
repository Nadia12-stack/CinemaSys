<<<<<<< HEAD
﻿using System.Threading.Tasks;

namespace CinemaSystem.Repositories
{
    public class MovieRepository:Repository<Movie>
    {
        private ApplicationDbContext _context = new();

        public async Task AddRangeAsync(IEnumerable<Movie> movies,CancellationToken cancellationToken =default)
        {
           await _context.AddRangeAsync(movies, cancellationToken);
        }
        public void Count( int count)
        {
            _context.Movies.Count();
        }
        
=======
﻿using CinemaSystem.Repositories;
using CinemaSystem.Repositories.IRepositories;
using System.Threading.Tasks;

namespace CinemaSystem.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken = default)
        {
            await _context.AddRangeAsync(movies, cancellationToken);
        }
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
    }
}
