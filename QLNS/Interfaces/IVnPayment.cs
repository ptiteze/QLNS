using QLNS.ViewModels.Order;

namespace QLNS.Interfaces
{
    public interface IVnPayment
    {
        Task<string> CreatePaymentUrl(int orderId);

        Task<VNPayResponseModel> LoadDataPaymentSuccess();
    }
}
