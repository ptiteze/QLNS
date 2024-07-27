using QLNS.Data;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Catalog;
using System.Net.Http.Json;

namespace QLNS.Repositories
{
    public class CatalogRepository : ICatalog
    {
        private readonly DataContext _dataContext;

		private readonly HttpClient _httpClient;

		public const string BaseUrl = "/api/Catalog";
		public CatalogRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddCatalog(string name)
        {
			HttpResponseMessage response = await _httpClient.PutAsync(BaseUrl + $"/{name}", null);
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

        public async Task<bool> DeleteCatalog(int id)
        {
			HttpResponseMessage response = await _httpClient.DeleteAsync(BaseUrl + $"/{id}");
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

        public async Task<List<CatalogDTO>> GetAllCatalog()
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<CatalogDTO>>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<CatalogDTO> GetCatalogById(int id)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<CatalogDTO>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<bool> UpdateCatalog(UpdateCatalogRequest request)
        {
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
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
