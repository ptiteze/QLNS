using QLNS.Interfaces;
using QLNS.Models;

namespace QLNS.Repositories
{
    public class SlideRepository : ISlide
    {
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Slide";
        public SlideRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
		public async Task<List<Slide>?> GetAllSlides()
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<Slide>>();
				return result;
			}
			else
			{
				return null;
			}
		}
    }
}
