using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;

namespace CinemaApp.Data.Repository
{
    public class ManagerRepository 
        : BaseRepository<Manager, Guid>, IManagerRepository
    {
        public ManagerRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
