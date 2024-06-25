using Projekt.SharedKernel.Dto.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Application.Services
{
    public interface IReviewService
    {
        List<ReviewDto> GetAll();
        ReviewDto GetById(int id);
        int Create(CreateReviewDto dto);
        void Update(UpdateReviewDto dto);
        void Delete(int id);

        List<ReviewDto> GetByMovieId(int movieId);
    }
}
