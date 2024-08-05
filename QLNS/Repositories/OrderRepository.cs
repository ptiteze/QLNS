using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Order;

namespace QLNS.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Order";
        public OrderRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateOrder(CreateOrderRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int>();
                return result;
            }
            else
            {
                return 0;
            }
        }

        public async Task<OrderDTO?> GetOrderById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/id/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OrderDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<OrderDTO>?> GetOrdersByUsername(string username)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/user/{username}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpDateOrder(OrderDTO request)
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

        public async Task<List<OrderDTO>> GetOrders()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
