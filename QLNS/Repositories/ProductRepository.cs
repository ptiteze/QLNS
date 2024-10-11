using Azure.Core;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Product;


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

		public async Task<bool> CreateProduct(InputProductRequest request)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + $"/create", request);
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

		public async Task<bool> UpdateProduct(UpdateProductRequest request)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl + $"/update", request);
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

		public async Task<bool> DeleteProduct(int id)
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
	}
}
