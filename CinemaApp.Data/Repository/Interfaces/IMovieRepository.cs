using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Interfaces
{
    public interface IMovieRepository : IRepository<Movie, Guid>, IAsyncRepository<Movie, Guid>
    {
    }
}
