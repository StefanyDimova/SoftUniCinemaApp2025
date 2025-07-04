using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Data.Common.EntityConstants.Cinema;

namespace CinemaApp.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> entity)
        {
            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(c => c.Location)
                .IsRequired(true)
                .HasMaxLength(LocationMaxLength);

            entity
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false);


            // това пропърти създава уникалност между име и локация на дадено кино , за да не се получават повторения
            entity
                .HasIndex(c => new { c.Name, c.Location })
                .IsUnique(true);

            entity
                .HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
