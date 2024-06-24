using Projekt.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance
{
    public class ProjektUnitOfWork: IProjektUnitOfWork
    {
        private readonly ProjektDbContext _context;
        public IMovieRepository MovieRepository { get; }
        public IMovieRoleRepository MovieRoleRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public IUserRepository UserRepository { get; }
        public ProjektUnitOfWork(ProjektDbContext context)
        {
            _context = context;
            MovieRepository = new MovieRepository(_context);
            MovieRoleRepository = new MovieRoleRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            UserRepository = new UserRepository(_context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
