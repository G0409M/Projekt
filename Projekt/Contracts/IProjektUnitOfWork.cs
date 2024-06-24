using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Contracts
{
    public interface IProjektUnitOfWork:IDisposable
    {
        IMovieRepository MovieRepository { get; }
        IMovieRoleRepository MovieRoleRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
