
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : BaseRepository<Movie, Guid>, IMovieRepository
    {
        public MovieRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
