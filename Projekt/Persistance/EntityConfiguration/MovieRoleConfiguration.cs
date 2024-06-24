using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance.EntityConfiguration
{
    public class MovieRoleConfiguration : IEntityTypeConfiguration<MovieRole>
    {
        public void Configure(EntityTypeBuilder<MovieRole> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedNever();

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.PersonName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.MovieId)
                .IsRequired();

            builder.Property<int>("RoleId");

            builder.HasOne<Movie>()
                .WithMany()
                .HasForeignKey("MovieId")
                .IsRequired();
        }
    }
}
