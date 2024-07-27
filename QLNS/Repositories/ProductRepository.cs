using QLNS.Data;
using QLNS.DTO;
using QLNS.Interfaces;


namespace QLNS.Repositories
{
    public class ProductRepository : IProduct
    {
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Product";
        public ProductRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
		public async Task<List<ProductDTO>?> GetAllProducts()
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<ProductDTO?> GetProductById(int id)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<ProductDTO>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<List<ProductDTO>?> GetProductsByCatalogId(int id)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/catalog/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
				return result;
			}
			else
			{
				return null;
			}
		}
    }
}
