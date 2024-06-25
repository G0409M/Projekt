using Newtonsoft.Json;
using Projekt.SharedKernel.Dto.Review;

namespace Projekt.BlazorUser.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        public ReviewService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEnumerable<ReviewDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ReviewDto>>(content);
                return products;
            }
            return new List<ReviewDto>();
        }
    }
}
