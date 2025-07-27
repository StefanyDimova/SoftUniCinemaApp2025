using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data.Models;

namespace CinemaApp.Services.Core
{
    public class ProjectionService : IProjectionService
    {
        private readonly ICinemaMovieRepository cinemaMovieRepository;

        public ProjectionService(ICinemaMovieRepository cinemaMovieRepository)
        {
            this.cinemaMovieRepository = cinemaMovieRepository;
        }
        public async Task<IEnumerable<string>> GetProjectionShowtimesAsync(string? cinemaId, string movieId)
        {
            IEnumerable<string> showtimes = new List<string>();
            if (!String.IsNullOrWhiteSpace(cinemaId) &&
                !String.IsNullOrWhiteSpace(cinemaId))
            {
                showtimes = await this.cinemaMovieRepository.GetAllAttached()
                    .Where(cm => cm.MovieId.ToString().ToLower() == movieId.ToLower() &&
                    cm.CinemaId.ToString().ToLower() == cinemaId.ToLower())
                    .Select(cm => cm.ShowTime)
                    .ToArrayAsync();
            }

            return showtimes;
        }

        public async Task<int> GetAvailableTicketsCountAsync(string? cinemaId, string? movieId, string? showtime)
        {
            int availableTicketsCount = 0;
            if (!String.IsNullOrWhiteSpace(cinemaId) &&
                !String.IsNullOrWhiteSpace(movieId) &&
                !String.IsNullOrWhiteSpace(showtime))
            {
            CinemaMovie? projection = await this.cinemaMovieRepository
                    .SingleOrDefaultAsync(cm => cm.CinemaId.ToString().ToLower() == cinemaId.ToLower() &&
                                           cm.MovieId.ToString().ToLower() == movieId.ToLower() &&
                                           cm.ShowTime == showtime);
                if (projection != null)
                {
                    availableTicketsCount = projection.AvailableTickets;
                }
            }

            return availableTicketsCount;
        }
    }
}
