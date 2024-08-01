using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Order;
using System.Net.Http;

namespace QLNS.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Transaction";
        public TransactionRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateTransaction(CreateTransactionRequest request)
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

        public async Task<TransactionDTO?> GetTransactionByOrderId(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TransactionDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<TransactionDTO?> GetTransactionByUserName(string userName)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"{userName}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TransactionDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
