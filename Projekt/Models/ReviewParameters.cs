using Projekt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class ReviewParameters : QueryStringParameters
    {
        public double? MinRating { get; set; }
        public double? MaxRating { get; set; }
    }
}
