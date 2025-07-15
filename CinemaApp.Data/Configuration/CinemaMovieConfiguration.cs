using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Data.Common.EntityConstants.CinemaMovie;

namespace CinemaApp.Data.Configuration
{
    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> entity)
        {
            entity
                .HasKey(cm => cm.Id);


            // Define pseudo-composite PK
            entity
                .HasIndex(cm => new { cm.MovieId, cm.CinemaId, cm.ShowTime})
                .IsUnique(true);

            entity
                .Property(cm => cm.IsDeleted)
                .HasDefaultValue(false);

            entity
                .Property(cm => cm.AvailableTickets)
                .HasDefaultValue(AvailableTicketsDefaultValue);

            entity
                .Property(cm => cm.ShowTime)
                .IsRequired(true)
                .HasMaxLength(ShowTimeMaxLength);

            entity
                .HasQueryFilter(cm => cm.IsDeleted == false && 
                                      cm.Movie.IsDeleted == false &&
                                      cm.Cinema.IsDeleted == false);

            entity
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.MovieProjections)
                .HasForeignKey(cm => cm.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(cm => cm.Cinema)
                .WithMany(cm => cm.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
