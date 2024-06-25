using Projekt.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Domain.Models
{
    public class MovieParameters : QueryStringParameters
    {
        public MovieGenre? MovieGenre { get; set; }
    }
}
