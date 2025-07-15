using CinemaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository.Interfaces
{
    public interface ICinemaRepository : IRepository<Cinema, Guid>, IAsyncRepository<Cinema, Guid>
    {
    }
}
