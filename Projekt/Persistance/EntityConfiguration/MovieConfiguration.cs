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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id_movie);

            builder.Property(m => m.Id_movie)
                .ValueGeneratedNever();

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.FilmLength)
                .IsRequired();

            builder.Property(m => m.Genre)
                .IsRequired();

            builder.Property(m => m.ReleaseDate)
                .IsRequired();


        }
    }
}
