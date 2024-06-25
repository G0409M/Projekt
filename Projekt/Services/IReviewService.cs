using Projekt.Dto.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public interface IReviewService
    {
        List<ReviewDto> GetAll();
        ReviewDto GetById(int id);
        int Create(CreateReviewDto dto);
        void Update(UpdateReviewDto dto);
        void Delete(int id);
    }
}
