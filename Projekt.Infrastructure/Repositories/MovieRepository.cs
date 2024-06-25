using Microsoft.EntityFrameworkCore;
using Projekt.Domain.Contracts;
using Projekt.Domain.Helpers;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Infrastructure.Repositories
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
        public int GetMaxId()
        {
            return _projektDbContext.Movies.Max(x => x.Id_movie);
        }
       
    }
}
