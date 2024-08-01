namespace QLNS.ModelsParameter.Order
{
    public class CreateOrderRequest
    {
        public DateOnly Sentdate { get; set; }

        public string Address { get; set; } = null!;

        public DateOnly? Receiveddate { get; set; }

        /// <summary>
        /// 0 : Đang chờ xác nhận, 1 Đang giao , 3 Dã nhận, 4 Đã hủy
        /// </summary>
        public int? Status { get; set; }

        public string UserName { get; set; } = null!;

        public int? AdminId { get; set; }
    }
}
