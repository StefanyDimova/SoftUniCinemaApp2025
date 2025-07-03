using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchlistRepository;
        private readonly CinemaAppDbContext dbContext;

        public WatchlistService(IWatchlistRepository watchlistRepository,CinemaAppDbContext dbContext)
        {
            this.watchlistRepository = watchlistRepository;
            this.dbContext = dbContext;
        }

        public async Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string? userId)
        {
            bool result = false;
            if (movieId != null && userId != null)
            {
                bool isMovieIdValid = Guid.TryParse(movieId, out Guid movieGuid);
                if (isMovieIdValid)
                {
                    ApplicationUserMovie? userMovieEntry = await this.dbContext
                        .ApplicationUserMovies
                        .IgnoreQueryFilters()
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.MovieId.ToString() == movieGuid.ToString());
                    if (userMovieEntry != null)
                    {
                        userMovieEntry.IsDeleted = false;
                    }
                    else
                    {
                        userMovieEntry = new ApplicationUserMovie()
                        {
                            ApplicationUserId = userId,
                            MovieId = movieGuid,
                        };

                        await this.dbContext.ApplicationUserMovies.AddAsync(userMovieEntry);
                    }

                    await this.dbContext.SaveChangesAsync();

                    result = true;
                }
            }

            return result;
        }

        //трябва да извлечем на даден потребител watchlist-a
        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            IEnumerable<WatchlistViewModel> userWatchlist = await this.dbContext
                .ApplicationUserMovies
                .Include(aum => aum.Movie)
                .AsNoTracking()
                .Where(aum => aum.ApplicationUserId.ToLower() == userId.ToLower())
                .Select(aum => new WatchlistViewModel()
                {
                    MovieId = aum.MovieId.ToString(),
                    Title = aum.Movie.Title,
                    Genre = aum.Movie.Genre,
                    ReleaseDate = aum.Movie.ReleaseDate.ToString(AppDateFormat),
                    ImageUrl = aum.Movie.ImageUrl ?? $"/images/{NoImageUrl}"
                })
                .ToArrayAsync();

            return userWatchlist;
        }

        public async Task<bool> RemoveMovieFromWatchlistAsync(string? movieId, string? userId)
        {
            bool result = false;
            if (movieId != null && userId != null)
            {
                bool isMovieIdValid = Guid.TryParse(movieId, out Guid movieGuid);
                if (isMovieIdValid)
                {
                    ApplicationUserMovie? userMovieEntry = await this.dbContext
                        .ApplicationUserMovies
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.MovieId.ToString() == movieGuid.ToString());
                    if (userMovieEntry != null)
                    {
                        userMovieEntry.IsDeleted = true;

                        await this.dbContext.SaveChangesAsync();

                        result = true;
                    }
                }
            }

            return result;
        }

        public async Task<bool> IsMovieAddedToWatchlist(string? movieId, string? userId)
        {
            bool result = false;
            if (movieId != null && userId != null)
            {
                bool isMovieIdValid = Guid.TryParse(movieId, out Guid movieGuid);
                if (isMovieIdValid)
                {
                    ApplicationUserMovie? userMovieEntry = await this.dbContext
                        .ApplicationUserMovies
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.MovieId.ToString() == movieGuid.ToString());
                    if (userMovieEntry != null)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}
