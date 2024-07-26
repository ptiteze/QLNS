namespace QLNS_BackEnd.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateOnly Sentdate { get; set; }

        public string Address { get; set; } = null!;

        public DateOnly? Receiveddate { get; set; }

        /// <summary>
        /// 0 : chưa hoàn thành, 1 hoàn thành
        /// </summary>
        public int? Status { get; set; }

        public string UserName { get; set; } = null!;

        public int? AdminId { get; set; }
    }
}
