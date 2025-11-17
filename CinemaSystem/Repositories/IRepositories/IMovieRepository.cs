using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movie>
    {


        Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken = default);

    }
}
