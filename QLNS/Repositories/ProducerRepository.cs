using Azure.Core;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Producer;

namespace QLNS.Repositories
{
    public class ProducerRepository : IProducer
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Producer";
        public ProducerRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateProducer(CreateProducerRequest request)
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

        public async Task<bool> DeleteProducer(int id)
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

        public async Task<List<ProducerDTO>?> GetAllProducer()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProducerDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<ProducerDTO?> GetProducerById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ProducerDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateProducer(ProducerDTO request)
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
