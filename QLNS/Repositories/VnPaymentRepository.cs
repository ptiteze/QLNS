using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ViewModels.Order;

namespace QLNS.Repositories
{
    public class VnPaymentRepository : IVnPayment
    {
        private readonly HttpClient _httpClient;
        public VnPaymentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }
        public async Task<string> CreatePaymentUrl(int orderId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Payments/" + $"{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<VNPayResponseModel> LoadDataPaymentSuccess()
        {
            return await _httpClient.GetFromJsonAsync<VNPayResponseModel>("/api/Payments/call-back");
        }
    }
}
