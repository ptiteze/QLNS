namespace QLNS_BackEnd.ModelsParameter.Order
{
    public class CreateOrderRequest
    {
        public DateOnly Sentdate { get; set; }

        public string Address { get; set; } = null!;

        public DateOnly? Receiveddate { get; set; }

        public string? UserName { get; set; }

        public string? UserMail { get; set; }

        public string? UserPhone { get; set; }

        public string? Payment { get; set; }

        public string? Message { get; set; }

        public int? Amount { get; set; }

        /// <summary>
        /// 0 : chưa hoàn thành, 1 hoàn thành
        /// </summary>
        public int? Status { get; set; }

        public int AdminId { get; set; }

        public int UserId { get; set; }
    }
}
