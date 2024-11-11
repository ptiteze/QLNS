

namespace QLNS_BackEnd.Services.PaymentService
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel request);

        Task<VNPayResponseModel> PaymentExecute(IQueryCollection collection);
    }
}
