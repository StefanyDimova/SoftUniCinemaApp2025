using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> entity)
        {
            entity
                .HasKey(m => m.Id);

            entity
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(m => m.User)
                .WithOne()
                .HasForeignKey<Manager>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasIndex(m => new { m.UserId })
                .IsUnique();

            entity
                .HasQueryFilter(m => m.IsDeleted == false);
        }
    }
}
