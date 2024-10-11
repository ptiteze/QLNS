using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.User;

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

        public async Task<bool> CreateUser(AddUser request)
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

        public async Task<bool> UpdateUser(UserDTO request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl + $"/update", request);
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

        public async Task<UserDTO> GetUserById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl+$"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserDTO>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
