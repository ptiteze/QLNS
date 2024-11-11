using Azure.Core;
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
	}
}
