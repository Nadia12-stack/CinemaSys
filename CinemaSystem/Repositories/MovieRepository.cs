using CinemaSystem.Repositories;
using CinemaSystem.Repositories.IRepositories;
using System.Threading.Tasks;

namespace CinemaSystem.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private ApplicationDbContext _context;

        public MovieRepository()
        {
        }

        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken = default)
        {
            await _context.AddRangeAsync(movies, cancellationToken);
        }
    }
}
