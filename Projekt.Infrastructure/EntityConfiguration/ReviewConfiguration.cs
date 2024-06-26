﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Infrastructure.EntityConfiguration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedNever();

            builder.Property(r => r.MovieId)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(500);

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.DateCreated)
                .IsRequired();

            builder.HasOne<Movie>()
                .WithMany()
                .HasForeignKey(r => r.MovieId)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired();
        }
    }
}
