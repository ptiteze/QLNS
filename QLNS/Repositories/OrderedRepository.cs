using QLNS.DTO;
using QLNS.Interfaces;

namespace QLNS.Repositories
{
    public class OrderedRepository : IOrdered
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Ordered";
        public OrderedRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<OrderedDTO>?> GetOrderedsByOrderId(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
            Console.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<OrderedDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        
    }
}
