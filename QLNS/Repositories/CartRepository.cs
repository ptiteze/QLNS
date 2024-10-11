using Microsoft.EntityFrameworkCore;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Cart;

namespace QLNS.Repositories
{
    public class CartRepository : ICart
    {
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Cart";
        public CartRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> AddProduct(RequestAddCart request)
        {
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl , request);
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

        public async Task<bool> CheckExistCart(RequestCheckCart request)
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

        public async Task<List<CartDTO>?> GetCartsByUserId(int id)
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<CartDTO>>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<bool> RemoveCart(RequestRemoveCart request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl+"/remove", request);
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
