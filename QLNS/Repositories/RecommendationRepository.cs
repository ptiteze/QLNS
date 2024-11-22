using Azure.Core;
using QLNS.DTO;
using QLNS.Interfaces;
using System.Text;

namespace QLNS.Repositories
{
	public class RecommendationRepository : IRecommendation
	{
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Recommendation";
		public RecommendationRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<string> GetUseds(List<int> listProduct)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, listProduct);
			if (response.IsSuccessStatusCode)
			{
				var contentStream = await response.Content.ReadAsStreamAsync();
				using (var reader = new StreamReader(contentStream, Encoding.UTF8))
				{
					var result = await reader.ReadToEndAsync();
					return result;
				}
			}
			else
			{
				return "";
			}
		}

		public async Task<bool> BuildDataset()
		{
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
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
