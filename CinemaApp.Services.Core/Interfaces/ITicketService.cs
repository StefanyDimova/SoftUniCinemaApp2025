using CinemaApp.Web.ViewModels.Ticket;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketIndexViewModel>> GetUserTicketsAsync(string? userId);

        Task<bool> AddTicketAsync(string? cinemaId, string? movieId, int quantity, string? showtime, string? userId);
    }
}
