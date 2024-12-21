using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.SupplyInvoice;

namespace QLNS.Repositories
{
    public class SupplyInvoiceRepository : ISupplyInvoice
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/SupplyInvoice";
        public SupplyInvoiceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateSupplyInvoice(CreateSupplyInvoiceRequest request)
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

        public async Task<List<SupplyInvoiceDTO>?> GetAllSupplyInvoice()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SupplyInvoiceDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<SupplyInvoiceDTO?> GetSupplyInvoiceById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SupplyInvoiceDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ViewSupply>> ViewSupplies()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+"/view");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ViewSupply>>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
