using Projekt.Helpers;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Contracts
{
    public interface IReviewRepository : IRepository<Review>
    {
        PagedList<Review> GetReviews(QueryStringParameters queryStringParameters);
        PagedList<Review> GetReviews(ReviewParameters parameters);
        int GetMaxId();
    }
}
