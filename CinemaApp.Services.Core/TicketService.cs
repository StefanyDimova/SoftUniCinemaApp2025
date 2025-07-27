using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Ticket;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }
        public async Task<IEnumerable<TicketIndexViewModel>> GetUserTicketsAsync(string userId)
        {
            IEnumerable<TicketIndexViewModel> userTickets = new List<TicketIndexViewModel>();

            if (!string.IsNullOrWhiteSpace(userId)) 
            {
                userTickets = await this.ticketRepository
                    .GetAllAttached()
                    .Where(x => x.UserId.ToLower() == userId.ToLower())
                    .Select(x => new TicketIndexViewModel()
                    {
                        MovieTitle = x.CinemaMovieProjection.Movie.Title,
                        MovieImageUrl = x.CinemaMovieProjection.Movie.ImageUrl ?? $"/images/{NoImageUrl}",
                        CinemaName = x.CinemaMovieProjection.Cinema.Name,
                        Showtime = x.CinemaMovieProjection.ShowTime,
                        TicketCount = x.Quantity,
                        TicketPrice = x.Price.ToString("F2"),
                        TotalPrice = (x.Quantity * x.Price).ToString("F2")
                    })
                    .ToArrayAsync();
            }

            return userTickets;
        }
    }
}
