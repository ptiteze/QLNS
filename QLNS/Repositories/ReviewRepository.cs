using Azure.Core;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Product;


namespace QLNS.Repositories
{
    public class ReviewRepository : IReview
    {
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Review";
		public ReviewRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<ReviewDTO?> GetReviewById(int id)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<ReviewDTO>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<List<ReviewDTO>?> GetReviewsByProductId(int ProductId)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/byproduct/{ProductId}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<ReviewDTO>>();
				
				return result;
			}
			else
			{
				return null;
			}
		}

        public Task<List<ReviewDTO>?> GetReviewsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewDTO> GetReview(InputGetReview input)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, input);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ReviewDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateReview(CreateReviewRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateReview(ReviewDTO request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl+"/update", request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
