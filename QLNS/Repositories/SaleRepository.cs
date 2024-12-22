using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Order;

namespace QLNS.Repositories
{
    public class SaleRepository : ISale
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Sale";
        public SaleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SaleDTO>> GetSales()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SaleDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<SaleDTO> GetSaleById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/sale/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SaleDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<SaleDetailDTO>> GetSaleDetailById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/detail/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SaleDetailDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateSale(CreateSaleRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl, request);
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
