using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.SharedKernel.Dto.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public TimeSpan FilmLength { get; set; }
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
