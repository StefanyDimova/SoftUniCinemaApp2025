using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Models
{
    [Comment("Ticket in the system")]
    public class Ticket
    {
        [Comment("Ticket identifier")]
        public Guid Id { get; set; }

        [Comment("Ticket price")]
        public decimal Price { get; set; }

        [Comment("Tickets quantity bought")]

        public int Quantity { get; set; }

        [Comment("Foreign key to the movie projection in a cinema")]
        public Guid CinemaMovieId { get; set; }

        public virtual CinemaMovie CinemaMovieProjection { get; set; } = null!;

        [Comment("Foreign key to the owner of the ticket")]
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;
    }
}
