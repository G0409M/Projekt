using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Domain.Models
{
    public enum MovieGenre
    {
        Action,
        Adventure,
        Animated,
        Comedy,
        Criminal,
        Family,
        Horror,
        Romantic,
        Thriller

    }
    public class Movie
    {
        public int Id_movie { get; set; }
        public double FilmLength { get; set; }
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
