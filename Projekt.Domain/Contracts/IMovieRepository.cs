using Projekt.Domain.Helpers;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Domain.Contracts
{
    public interface IMovieRepository : IRepository<Movie>
    {
        PagedList<Movie> GetMovies(QueryStringParameters queryStringParameters);
        PagedList<Movie> GetMovies(MovieParameters parameters);
        int GetMaxId();
    }
}
