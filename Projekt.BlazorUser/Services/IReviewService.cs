using Projekt.SharedKernel.Dto.Review;

namespace Projekt.BlazorUser.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAll();
    }
}
