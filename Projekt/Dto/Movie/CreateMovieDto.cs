﻿using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Dto.Movie
{
    public class CreateMovieDto
    {
        public TimeSpan FilmLength { get; set; }
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
