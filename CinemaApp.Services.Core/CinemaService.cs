using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }
        public async Task<IEnumerable<UsersCinemaIndexViewModel>> GetAllCinemasUserViewAsync()
        {
            IEnumerable<UsersCinemaIndexViewModel> allCinemasUsersView = await this.cinemaRepository
                .GetAllAttached()
                .Select(c => new UsersCinemaIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                })
                .ToArrayAsync();

            return allCinemasUsersView;
        }


        public async Task<CinemaProgramViewModel?> GetCinemaProgramAsync(string? cinemaId)
        {
            CinemaProgramViewModel cinemaProgram = null;

            if (!string.IsNullOrWhiteSpace(cinemaId))
            {
                Cinema? cinema = await this.cinemaRepository
                    .GetAllAttached()
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == cinemaId.ToLower());

                if (cinema != null)
                {
                    cinemaProgram = new CinemaProgramViewModel()
                    {
                        CinemaId = cinema.Id.ToString(),
                        CinemaName = cinema.Name,
                        CinemaData = cinema.Name + " - " + cinema.Location,
                        Movies = cinema.CinemaMovies
                        .Select(cm => cm.Movie)
                        .Select(m => new CinemaProgramMovieViewModel()
                        {
                            Id = m.Id.ToString(),
                            Title = m.Title,
                            Director = m.Director,
                            ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}"
                        })
                        .ToArray()
                    };
                }
            }

            return cinemaProgram;
        }


        public async Task<CinemaDetailsViewModel?> GetCinemaDetailsAsync(string? cinemaId)
        {
            CinemaDetailsViewModel? cinemaDetails = null;
            if (!String.IsNullOrWhiteSpace(cinemaId))
            {
                Cinema? cinema = await this.cinemaRepository
                    .GetAllAttached()
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == cinemaId.ToLower());
                if (cinema != null)
                {
                    cinemaDetails = new CinemaDetailsViewModel()
                    {
                        Name = cinema.Name,
                        Location = cinema.Location,
                        Movies = cinema.CinemaMovies
                            .Select(cm => cm.Movie)
                            .DistinctBy(m => m.Id)
                            .Select(m => new CinemaDetailsMovieViewModel()
                            {
                                Title = m.Title,
                                Duration = m.Duration.ToString(),
                            })
                            .ToArray(),
                    };
                }
            }

            return cinemaDetails;

        }
    }
}
