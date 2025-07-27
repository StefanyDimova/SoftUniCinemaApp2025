using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IProjectionService
    {
        Task<IEnumerable<string>> GetProjectionShowtimesAsync(string? cinemaId, string movieId);

        Task<int> GetAvailableTicketsCountAsync(string? cinemaId, string? movieId, string? showtime);
    }
}
