using Projekt.Contracts;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance
{
    public class MovieRoleRepository : Repository<MovieRole>, IMovieRoleRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public MovieRoleRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
    }
}
