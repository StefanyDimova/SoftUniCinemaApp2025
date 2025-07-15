namespace CinemaApp.Data.Repository.Interfaces
{
    using CinemaApp.Data.Models;
    public interface IManagerRepository : IRepository<Manager, Guid>, IAsyncRepository<Manager, Guid>
    {

    }
}
