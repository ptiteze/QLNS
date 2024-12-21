namespace QLNS.ModelsParameter.SupplyInvoice
{
    public class ViewSupply
    {
        public int Id { get; set; }

        public DateOnly SupplyTime { get; set; }

        public int AdId { get; set; } = 0;

        public int ProducerId { get; set; }

        public string CatalogIn { get; set; }

        public string ProductIn { get; set; }

        public int Amount { get; set; } = 0;
    }
}
