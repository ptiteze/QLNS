using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.Services.PaymentService;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrder _order;

        public PaymentsController(IVnPayService vnPayService, IOrder order) 
        {
            _vnPayService = vnPayService;
            _order = order;
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> CreatePaypent(int orderId) 
        {   
            var order = await SingletonDataBridge.GetInstance().Orders.FindAsync(orderId);
            if(order == null) 
            {
                var errorResponse = new
                {
                    message = "Không thể lấy dữ liệu",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }

            if(order.Status == 3)
            {
                var errorResponse = new
                {
                    message = "Đơn hàng đã bị hủy",
                    status = "error",
                    errorCode = 400
                };
                return BadRequest(errorResponse);
            }
            if (order.Status == 2)
            {
                var errorResponse = new
                {
                    message = "Đơn hàng đã được thanh toán",
                    status = "error",
                    errorCode = 400
                };
                return BadRequest(errorResponse);
            }
            //edit
            decimal total = order.Amount.Value;
            var customer = await SingletonDataBridge.GetInstance().Users.FindAsync(order.UserId);
            if (customer == null)
            {
                var errorResponse = new
                {
                    message = "Không tìm thấy người dùng",
                    status = "error",
                    errorCode = 400
                };
                return BadRequest(errorResponse);
            }
            VnPaymentRequestModel model = new VnPaymentRequestModel() 
            {
                Amount = total,
                OrderId = orderId.ToString(),
                Description = "Thanh Toán Đơn Hàng qua VNPAY",
                FullName = customer.Nameuser
            };
            string res = await _vnPayService.CreatePaymentUrl(HttpContext, model);
            return Ok(res); 
        }
        //Giao dịch thành công thì load về trang này cập nhật trạng thái đơn hàng
        [HttpGet("call-back")]
        public async Task<IActionResult> PaymentCallBack()
        {
            try
            {
                var response = await _vnPayService.PaymentExecute(Request.Query);
                if (response == null || response.VnPayResponseCode != "00")
                {
                    var errorResponse = new
                    {
                        message = "Không thể thực hiện giao dịch",
                        status = "error",
                        errorCode = 400
                    };
                    return BadRequest(errorResponse);
                }
                OrderDTO request = await _order.GetOrderById(int.Parse(response.OrderId));
                Console.WriteLine(request.Payment+"--"+request.Id);
                request.Status = 1;
                request.Payment = "Thanh toán VNPay";
                bool check = await _order.UpDateOrder(request);
                if (!check)
                {
                    var errorResponse = new
                    {
                        message = "Không thể thực hiện giao dịch",
                        status = "error",
                        errorCode = 400
                    };
                    return BadRequest(errorResponse);
                }
                return Ok(response);
            }
            catch(Exception ex) 
            {
                var errorResponse = new
                {
                    message = "Không thể thực hiện giao dịch",
                    status = "error",
                    errorCode = 400
                };
                return BadRequest(errorResponse);
            }
        }
    }
}
