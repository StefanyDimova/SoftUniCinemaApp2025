namespace CinemaApp.Services.Core.Interfaces
{
    using CinemaApp.Web.ViewModels.Movie;
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task AddMovieAsync(MovieFormInputModel inputModel);

        //покажи детайлите за филма по ид-то
        Task<MovieDetailsViewModel> GetMovieDetailsByIdAsync(string? id);


        Task<MovieFormInputModel?> GetEditableMovieByIdAsync(string? id);

        Task<bool> EditMovieAsync(MovieFormInputModel inputModel);

        Task<bool> SoftDeleteMovieAsync(string? id);
        Task<bool> DeleteMovieAsync(string? id);
        Task<DeleteMovieViewModel?> GetMovieDeleteDetailsByIdAsync(string? id);
    }
}
