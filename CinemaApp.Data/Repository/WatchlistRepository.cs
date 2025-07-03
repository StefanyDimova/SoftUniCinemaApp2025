using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class WatchlistRepository : BaseRepository<ApplicationUserMovie, object>, IWatchlistRepository
    {
        public WatchlistRepository(CinemaAppDbContext dbContext) : base(dbContext)
        {
        }

        public bool Exists(string userId, string movieId)
        {
            return this
                .GetAllAttached()
                .Any(am => am.ApplicationUserId.ToLower() == (userId.ToLower()) &&
                                am.MovieId.ToString().ToLower() == (movieId.ToLower()));
        }

        public Task<bool> ExistsAsync(string userId, string movieId)
        {
            return this
                .GetAllAttached()
                .AnyAsync(am => am.ApplicationUserId.ToLower() == (userId.ToLower()) &&
                                am.MovieId.ToString().ToLower() == (movieId.ToLower()));
        }

        public ApplicationUserMovie? GetByCompositeKey(string userId, string movieId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefault(am => am.ApplicationUserId.ToLower() == (userId.ToLower()) &&
                                am.MovieId.ToString().ToLower() == (movieId.ToLower()));
        }

        public Task<ApplicationUserMovie?> GetByCompositeKeyAsync(string userId, string movieId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefaultAsync(am => am.ApplicationUserId.ToLower() == (userId.ToLower()) &&
                                am.MovieId.ToString().ToLower() == (movieId.ToLower()));
        }
    }
}
