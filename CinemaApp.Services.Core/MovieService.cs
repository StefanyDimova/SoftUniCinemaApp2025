using CinemaApp.Data;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.VisualBasic;
using static CinemaApp.GCommon.ApplicationConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data.Models;
using System.Globalization;
using CinemaApp.Data.Repository.Interfaces;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;


        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
      

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies = await this.movieRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(movie => new AllMoviesIndexViewModel()
                {
                    Id = movie.Id.ToString(),
                    Title = movie.Title,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate.ToString(AppDateFormat), 
                    Director = movie.Director,
                    ImageUrl = movie.ImageUrl
                })
                .ToListAsync();
            foreach (AllMoviesIndexViewModel movie in allMovies)
            {
                if (String.IsNullOrEmpty(movie.ImageUrl))
                {
                    movie.ImageUrl = $"/images/{NoImageUrl}";
                }
            }

            return allMovies;
        }

        //от input model трябва да създадем един нов филм , да го добавим и да запазим промените
        public async Task AddMovieAsync(MovieFormInputModel inputModel)
        {
            Movie newMovie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                Director = inputModel.Director,
                Description = inputModel.Description,
                Duration = inputModel.Duration,
                ImageUrl = inputModel.ImageUrl,
                ReleaseDate = DateOnly.ParseExact(inputModel.ReleaseDate, $"{AppDateFormat}", CultureInfo.InvariantCulture, DateTimeStyles.None)
            };

            await this.movieRepository.AddAsync(newMovie);
        }

        public async Task<MovieDetailsViewModel> GetMovieDetailsByIdAsync(string? id)
        {
            MovieDetailsViewModel? movieDetails = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid movieId);

            if (isIdValidGuid)
            {
                movieDetails = await this.movieRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(m => m.Id == movieId)
                    .Select(m => new MovieDetailsViewModel()
                    {
                        Id = m.Id.ToString(),
                        Description = m.Description,
                        Director = m.Director,
                        Duration = m.Duration,
                        Genre = m.Genre,
                        ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}",
                        ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                        Title = m.Title
                    })
                    .SingleOrDefaultAsync();
            }

            return movieDetails;
        }


        public async Task<bool> EditMovieAsync(MovieFormInputModel inputModel)
        {
            Movie? editableMovie = await this.FindMovieByStringId(inputModel.Id);

            bool result = false;
            if (editableMovie == null)
            {
                return false;
            }

            DateOnly movieReleaseDate = DateOnly
                .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            editableMovie.Title = inputModel.Title;
            editableMovie.Description = inputModel.Description;
            editableMovie.Director = inputModel.Director;
            editableMovie.Duration = inputModel.Duration;
            editableMovie.Genre = inputModel.Genre;
            editableMovie.ImageUrl = inputModel.ImageUrl ?? $"/images/{NoImageUrl}";
            editableMovie.ReleaseDate = movieReleaseDate;

            result = await this.movieRepository.UpdateAsync(editableMovie);

            return result;
        }

        public async Task<MovieFormInputModel?> GetEditableMovieByIdAsync(string? id)
        {
            MovieFormInputModel? editableMovie = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid movieId);
            if (isIdValidGuid)
            {
                editableMovie = await this.movieRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(m => m.Id == movieId)
                    .Select(m => new MovieFormInputModel()
                    {
                        Description = m.Description,
                        Director = m.Director,
                        Duration = m.Duration,
                        Genre = m.Genre,
                        ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}",
                        ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                        Title = m.Title
                    })
                    .SingleOrDefaultAsync();
            }

            return editableMovie;
        }

        public async Task<DeleteMovieViewModel?> GetMovieDeleteDetailsByIdAsync(string? id)
        {
            DeleteMovieViewModel? deleteMovieViewModel = null;

            Movie? movieToBeDeleted = await this.FindMovieByStringId(id);
            if (movieToBeDeleted != null)
            {
                deleteMovieViewModel = new DeleteMovieViewModel()
                {
                    Id = movieToBeDeleted.Id.ToString(),
                    Title = movieToBeDeleted.Title,
                    ImageUrl = movieToBeDeleted.ImageUrl ?? $"/images/{NoImageUrl}",
                };
            }

            return deleteMovieViewModel;
        }


        public async Task<bool> SoftDeleteMovieAsync(string? id)
        {
            bool result = false;
            Movie? movieToDelete = await this.FindMovieByStringId(id);



            if (movieToDelete == null)
            {
                return false;
            }

            result = await this.movieRepository.DeleteAsync(movieToDelete);
            return result;
        }

        public async Task<bool> DeleteMovieAsync(string? id)
        {
            

            Movie? movieToDelete = await this.FindMovieByStringId(id);

            if (movieToDelete == null)
            {
                return false;
            }
            await this.movieRepository.HardDeleteAsync(movieToDelete);

            return true;
        }

        private async Task<Movie?> FindMovieByStringId(string? id)
        {
            Movie? movie = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                bool isGuidValid = Guid.TryParse(id, out Guid movieGuid);
                if (isGuidValid)
                {
                    movie = await this.movieRepository.GetByIdAsync(movieGuid);
                }
            }

            return movie;
        }
    }
}
