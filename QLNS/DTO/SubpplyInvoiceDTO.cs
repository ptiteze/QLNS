namespace QLNS.DTO
{
    public class SubpplyInvoiceDTO
    {
        public int Id { get; set; }

        public DateOnly SupplyTime { get; set; }

        public string AdId { get; set; } = null!;

        public int ProducerId { get; set; }
    }
}
