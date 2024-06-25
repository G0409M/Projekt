using Microsoft.EntityFrameworkCore;
using Projekt.Domain.Models;
using Projekt.Infrastructure.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Infrastructure
{
    public class ProjektDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRole> MovieRoles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public ProjektDbContext(DbContextOptions<ProjektDbContext> options) : base(options)
        {
            
            Database.EnsureDeleted();
            Database.EnsureCreated();

            DataSeeder.SeedData(this);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new MovieConfiguration());
            builder.ApplyConfiguration(new MovieRoleConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }

    }
}

