using CinemaApp.Web.ViewModels.Watchlist;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

        Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string? userId);

        Task<bool> RemoveMovieFromWatchlistAsync(string? movieId, string? userId);

        Task<bool> IsMovieAddedToWatchlist(string? movieId, string? userId);
    }
}
