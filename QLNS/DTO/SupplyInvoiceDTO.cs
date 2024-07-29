namespace QLNS.DTO
{
    public class SupplyInvoiceDTO
    {
        public int Id { get; set; }

        public DateOnly SupplyTime { get; set; }

        public int AdId { get; set; } = 0;

        public int ProducerId { get; set; }
    }
}
