using QLNS.Interfaces;
using QLNS.Models;


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

		public async Task<Review?> GetReviewById(int id)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<Review>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<List<Review>?> GetReviewsByProductId(int ProductId)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/byproduct/{ProductId}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<Review>>();
				
				return result;
			}
			else
			{
				return null;
			}
		}

        public Task<List<Review>?> GetReviewsByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
