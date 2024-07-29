namespace QLNS_BackEnd.ModelsParameter.SupplyInvoice
{
    public class CreateSupplyInvoiceRequest
    {
        public DateOnly SupplyTime { get; set; }

        public int AdId { get; set; } = 0;

        public int ProducerId { get; set; }
    }
}
