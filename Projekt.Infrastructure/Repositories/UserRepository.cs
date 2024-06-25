using Projekt.Domain.Contracts;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public UserRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
        public int GetMaxId()
        {
            return _projektDbContext.Users.Max(x => x.Id);
        }
    }
}
