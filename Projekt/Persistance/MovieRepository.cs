using Microsoft.EntityFrameworkCore;
using Projekt.Contracts;
using Projekt.Helpers;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public MovieRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
        public PagedList<Movie> GetMovies(QueryStringParameters queryStringParameters)
        {
            var query = _projektDbContext.Movies.AsNoTracking();
            return query.AsPagedList(queryStringParameters.PageNumber, queryStringParameters.PageSize);
        }

        public PagedList<Movie> GetMovies(MovieParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
