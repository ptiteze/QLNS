using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Admin;

namespace QLNS.Repositories
{
    public class UserRepository : IUser
    {
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/User";
        public UserRepository(HttpClient httpClient) {  
            _httpClient = httpClient;
        }

		public async Task<List<UserDTO>?> GetUsers()
        {
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<UserDTO>>();
				return result;
			}
			else
			{
				return null;
			}
		}

        public async Task<bool> LockUser(string username)
        {
			HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + $"/lock/{username}", null);
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

        public async Task<bool> UnLockUser(string username)
        {
			HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + $"/unlock/{username}", null);
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
