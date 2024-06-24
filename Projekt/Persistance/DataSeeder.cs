using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance
{
    public class DataSeeder
    {
        public static void SeedData(ProjektDbContext context)
        {
            SeedMovies(context);
            SeedUsers(context);
            SeedMovieRoles(context);
            SeedReviews(context);
        }

        private static void SeedMovies(ProjektDbContext context)
        {
            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Id_movie = 1,
                        Title = "The Shawshank Redemption",
                        FilmLength = TimeSpan.FromMinutes(142),
                        Genre = MovieGenre.Criminal,
                        ReleaseDate = new DateTime(1994, 10, 14),
                    },

                    new Movie
                    {
                        Id_movie = 2,
                        Title = "The Godfather",
                        FilmLength = TimeSpan.FromMinutes(175),
                        Genre = MovieGenre.Horror,
                        ReleaseDate = new DateTime(1972, 3, 24),
                    },

                    new Movie
                    {
                        Id_movie = 3,
                        Title = "The Dark Knight",
                        FilmLength = TimeSpan.FromMinutes(152),
                        Genre = MovieGenre.Action,
                        ReleaseDate = new DateTime(2008, 7, 18),
                    },
                };

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }

        private static void SeedUsers(ProjektDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = 1,
                        Username = "john.doe",
                        Email = "john.doe@example.com",
                        PasswordHash = "hashedpassword1"
                    },
                    new User
                    {
                        Id = 2,
                        Username = "jane.smith",
                        Email = "jane.smith@example.com",
                        PasswordHash = "hashedpassword2"
                    },
                    new User
                    {
                        Id = 3,
                        Username = "jack.jones",
                        Email = "jack.jones@example.com",
                        PasswordHash = "hashedpassword3"
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private static void SeedMovieRoles(ProjektDbContext context)
        {
            if (!context.MovieRoles.Any())
            {
                var movies = context.Movies.ToList();

                var roles = new List<MovieRole>
                {
                    new MovieRole
                    {
                        Id = 1,
                        RoleName = "Andy Dufresne",
                        PersonName = "Tim Robbins",
                        MovieId = movies.FirstOrDefault(m => m.Title == "The Shawshank Redemption").Id_movie,
                        RoleType = RoleType.Actor
                    },

                    new MovieRole
                    {
                        Id = 2,
                        RoleName = "Michael Corleone",
                        PersonName = "Al Pacino",
                        MovieId = movies.FirstOrDefault(m => m.Title == "The Godfather").Id_movie,
                        RoleType = RoleType.Actor
                    },

                    new MovieRole
                    {
                        Id = 3,
                        RoleName = "Batman",
                        PersonName = "Christian Bale",
                        MovieId = movies.FirstOrDefault(m => m.Title == "The Dark Knight").Id_movie,
                        RoleType = RoleType.Actor
                    },
                };

                context.MovieRoles.AddRange(roles);
                context.SaveChanges();
            }
        }

        private static void SeedReviews(ProjektDbContext context)
        {
            if (!context.Reviews.Any())
            {
                var movies = context.Movies.ToList();
                var users = context.Users.ToList();

                var theShawshankRedemption = movies.FirstOrDefault(m => m.Title == "The Shawshank Redemption");
                var theGodfather = movies.FirstOrDefault(m => m.Title == "The Godfather");
                var theDarkKnight = movies.FirstOrDefault(m => m.Title == "The Dark Knight");

                var johnDoe = users.FirstOrDefault(u => u.Username == "john.doe");
                var janeSmith = users.FirstOrDefault(u => u.Username == "jane.smith");
                var jackJones = users.FirstOrDefault(u => u.Username == "jack.jones");

                if (theShawshankRedemption == null || theGodfather == null || theDarkKnight == null || johnDoe == null || janeSmith == null || jackJones == null)
                {
                    throw new Exception("One or more required movies or users were not found in the database.");
                }

                var reviews = new List<Review>
                {
                    new Review
                    {
                        Id = 1,
                        MovieId = theShawshankRedemption.Id_movie,
                        UserId = johnDoe.Id,
                        Comment = "A masterpiece!",
                        Rating = 5,
                        DateCreated = DateTime.Now
                    },
                    new Review
                    {
                        Id = 2,
                        MovieId = theGodfather.Id_movie,
                        UserId = janeSmith.Id,
                        Comment = "Excellent movie.",
                        Rating = 4,
                        DateCreated = DateTime.Now.AddHours(-1)
                    },
                    new Review
                    {
                        Id = 3,
                        MovieId = theDarkKnight.Id_movie,
                        UserId = jackJones.Id,
                        Comment = "Awesome!",
                        Rating = 5,
                        DateCreated = DateTime.Now.AddHours(-2)
                    }
                };

                context.Reviews.AddRange(reviews);
                context.SaveChanges();
            }
        }
    }
}
