using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.SupplyList;

namespace QLNS.Repositories
{
    public class SupplyListRepository : ISupplyList
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/SupplyList";
        public SupplyListRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateSupplyList(CreateSupplyListRequest request)
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

        public async Task<bool> DeleteSupplyList(int id)
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

        public async Task<List<SupplyListDTO>> GetAllSupplyList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SupplyListDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateSupplyList(CreateSupplyListRequest request)
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
