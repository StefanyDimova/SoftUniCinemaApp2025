using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity
                .HasKey(t => t.Id);

            entity
                .Property(t => t.Price)
                .HasColumnType(PriceSqlType);

            entity
                .Property(cm => cm.UserId)
                .IsRequired(true);

            entity
                .HasOne(t => t.CinemaMovieProjection)
                .WithMany(cm => cm.Tickets)
                .HasForeignKey(cm => cm.CinemaMovieId);

            entity
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(cm => cm.UserId);

            entity
                .HasIndex(t => new { t.CinemaMovieId, t.UserId })
                .IsUnique(true);

            entity
                .HasQueryFilter(t => t.CinemaMovieProjection.IsDeleted == false);
        }
    }
}
