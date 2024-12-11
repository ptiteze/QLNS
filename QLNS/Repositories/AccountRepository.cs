using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Admin;

namespace QLNS.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/Account";
        public AccountRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<InfoLogin> Login(RequestLogin request)
        {

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<InfoLogin>();
                return result;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> Lock(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/lock/{id}");
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

        public async Task<bool> UnLock(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/unlock/{id}");
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

        public async Task<AccountDTO> GetAccountByUsername(string username)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/{username}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AccountDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<AccountDTO>> GetAccounts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<AccountDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ForgetPass(RequestForgetPass request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl+"/forgetpass", request);
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
