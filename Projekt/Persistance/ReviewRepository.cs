using Projekt.Contracts;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Persistance
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public ReviewRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
    }
}
