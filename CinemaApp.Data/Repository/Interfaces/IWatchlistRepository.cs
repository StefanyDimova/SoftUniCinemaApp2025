using CinemaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository.Interfaces
{
    public interface IWatchlistRepository : IRepository<ApplicationUserMovie, object>,
        IAsyncRepository<ApplicationUserMovie, object>
    {
        ApplicationUserMovie? GetByCompositeKey(string userId, string movieId);
        Task<ApplicationUserMovie?> GetByCompositeKeyAsync(string userId, string movieId);

        bool Exists(string userId, string movieId);

        Task<bool> ExistsAsync(string userId, string movieId);
    }
}
