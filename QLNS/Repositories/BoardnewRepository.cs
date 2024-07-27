using QLNS.Interfaces;
using QLNS.Models;

namespace QLNS.Repositories
{
	public class BoardnewRepository : IBoardnew
	{
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Boardnew";
		public BoardnewRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<Boardnew> GetBoardnewById(int id)
        {
			HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + $"/{id}", null);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<Boardnew>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<List<Boardnew>> GetBoardnews()
		{
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<Boardnew>>();
				return result;
			}
			else
			{
				return null;
			}
		}
	}
}
