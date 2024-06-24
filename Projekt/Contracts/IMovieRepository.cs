using Projekt.Helpers;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Contracts
{
    public interface IMovieRepository : IRepository<Movie>
    {
        PagedList<Movie> GetMovies(QueryStringParameters queryStringParameters);
        PagedList<Movie> GetMovies(MovieParameters parameters);
    }
}
