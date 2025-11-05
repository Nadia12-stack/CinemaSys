using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
<<<<<<< HEAD
        Task AddRangeAsync(IEnumerable<Movie> products, CancellationToken cancellationToken = default);
=======
        Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken = default);
>>>>>>> e4955d5d839d0b832f284b280ce538c034a76a5f
    }
}
