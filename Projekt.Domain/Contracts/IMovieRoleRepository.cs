using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Domain.Contracts
{
    public interface IMovieRoleRepository : IRepository<MovieRole>
    {
        int GetMaxId();
    }
}
