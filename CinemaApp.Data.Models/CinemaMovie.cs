using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    [Comment("Movie projection in a cinema in the system")]
    public class CinemaMovie
    {
        [Comment("Movie projection identifier")]
        public Guid Id { get; set; }

        [Comment("Foreign key to the movie")]
        public Guid MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        [Comment("Foreign key to the cinema")]
        public Guid CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; } = null!;

        [Comment("Count of available tickets")]
        public int AvailableTickets { get; set; }

        [Comment("Shows if the movie projection in the cinema is active")]
        public bool IsDeleted { get; set; }

        [Comment("String indicating the showtime of the Movie projection")]
        public string ShowTime { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } 
                = new HashSet<Ticket>();
    }
}
