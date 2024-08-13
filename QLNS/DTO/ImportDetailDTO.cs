namespace QLNS.DTO
{
    public class ImportDetailDTO
    {
        public int InvoiceId { get; set; }

        public int ProductId { get; set; }

        public int ImportPrice { get; set; }

        public int QuantityImport { get; set; }

        public int? Stock { get; set; }

        public bool? Status { get; set; }

    }
}
