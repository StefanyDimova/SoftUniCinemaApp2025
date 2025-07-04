using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    [Comment("Cinema in the system")]
    public class Cinema
    {
        [Comment("Cinema identifier")]
        public Guid Id { get; set; }

        [Comment("Cinema name")]
        public string Name { get; set; } = null!;

        [Comment("Cinema Location")]
        public string Location { get; set; } = null!;

        [Comment("Shows if cinema is deleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } 
                  = new HashSet<CinemaMovie>();
    }
}
