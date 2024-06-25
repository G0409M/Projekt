using Microsoft.EntityFrameworkCore;
using Projekt.Domain.Contracts;
using Projekt.Domain.Helpers;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public ReviewRepository(ProjektDbContext context)
            : base(context)
        {
            _projektDbContext = context;
        }
        public PagedList<Review> GetReviews(QueryStringParameters queryStringParameters)
        {
            var query = _projektDbContext.Reviews.AsNoTracking();
            return query.AsPagedList(queryStringParameters.PageNumber, queryStringParameters.PageSize);
        }

        public PagedList<Review> GetReviews(ReviewParameters pp)
        {
            // get queryable data
            var query = _projektDbContext.Reviews
            .AsNoTracking();
            // filtering
            if (pp.MinRating.HasValue)
            {
                query = query.Where(p => p.Rating >= pp.MinRating.Value);
            }
            if (pp.MaxRating.HasValue)
            {
                query = query.Where(p => p.Rating <= pp.MaxRating.Value);
            }
            // sorting
            if (!string.IsNullOrEmpty(pp.SortColumn))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Review, object>>>
                    {
                    { nameof(Review.Id), p => p.Id},
                    { nameof(Review.MovieId), p => p.MovieId},
                    { nameof(Review.UserId), p => p.UserId},
                    { nameof(Review.Comment), p => p.Comment},
                    { nameof(Review.Rating), p => p.Rating},
                    { nameof(Review.DateCreated), p => p.DateCreated},
                    };
                if (columnSelector.ContainsKey(pp.SortColumn))
                {
                    var selectedColumn = columnSelector[pp.SortColumn];
                    query = pp.SortDirection == SortDirection.ASC
                    ? query.OrderBy(selectedColumn)
                    : query.OrderByDescending(selectedColumn);
                }
            }

            // paging
            return query.AsPagedList(pp.PageNumber, pp.PageSize);
        }

        public int GetMaxId()
        {
            return _projektDbContext.Reviews.Max(x => x.Id);
        }
    }
}
