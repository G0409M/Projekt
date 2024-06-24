﻿using Projekt.Contracts;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public UserRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
    }
}
