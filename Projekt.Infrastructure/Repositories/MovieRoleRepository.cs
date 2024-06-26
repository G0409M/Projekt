﻿using Projekt.Domain.Contracts;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Infrastructure.Repositories
{
    public class MovieRoleRepository : Repository<MovieRole>, IMovieRoleRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public MovieRoleRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
        public int GetMaxId()
        {
            return _projektDbContext.MovieRoles.Max(x => x.Id);
        }
    }
}
