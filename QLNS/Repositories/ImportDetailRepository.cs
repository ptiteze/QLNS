using QLNS.DTO;
using QLNS.Interfaces;

namespace QLNS.Repositories
{
    public class ImportDetailRepository : IImportDetail
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "/api/ImportDetail";
        public ImportDetailRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ImportDetailDTO>?> GetImportDetailsBySupplyId(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ImportDetailDTO>>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
