using Projekt.Domain.Helpers;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Domain.Contracts
{
    public interface IReviewRepository : IRepository<Review>
    {
        PagedList<Review> GetReviews(QueryStringParameters queryStringParameters);
        PagedList<Review> GetReviews(ReviewParameters parameters);
        int GetMaxId();
    }
}
