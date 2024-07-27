using Azure.Core;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Admin;
using System.Net.Http.Json;

namespace QLNS.Repositories
{
	public class AdminRepository : IAdmin
	{
		private readonly HttpClient _httpClient;

		private const string BaseUrl = "/api/Admin";
		public AdminRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<bool> CheckExits(RequestCheckAdmin request)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl + "/check", request);
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

		public async Task<bool> CreateAdmin(AddAdmin request)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + "/create", request);
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

		public async Task<bool> DeleteAdmin(int id)
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

		public async Task<AdminDTO> GetAdmin(int id)
		{
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/get/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<AdminDTO>();
				return result;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<AdminDTO>?> GetAdmins()
		{
			HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<AdminDTO>>();
				return result;
			}
			else
			{
				return null;
			}
		}

		public async Task<InfoLogin> Login(RequestLogin request)
		{

			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl + "/login", request);
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

		public async Task<bool> UnLockAdmin(int id)
		{

			HttpResponseMessage response = await _httpClient.PutAsync(BaseUrl + $"/unlock/{id}", null);
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

		public async Task<bool> UpdateAdmin(UpdateAdmin request)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + "/update", request);
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
