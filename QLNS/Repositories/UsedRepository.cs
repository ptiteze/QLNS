using Azure.Core;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Product;

namespace QLNS.Repositories
{
    public class UsedRepository : IUsed
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Used";
        public UsedRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateUsed(string usedname)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/create/{usedname}");
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

        public async Task<bool> CreateUsedProduct(UsedProductRequest request)
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

        public async Task<bool> DeleteUsed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(BaseUrl + $"/used/{id}");
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

        public async Task<bool> DeleteUsedProduct(UsedProductRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl+"/delete", request);
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

        public async Task<List<UsedDTO>> GetAllUseds()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UsedDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<UsedDTO> GetUsedById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UsedDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UsedDTO>> GetUsedsByProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UsedDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
