using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository
{
    public class CinemaMovieRepository : BaseRepository<CinemaMovie, Guid>, ICinemaMovieRepository
    {
        public CinemaMovieRepository(CinemaAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
