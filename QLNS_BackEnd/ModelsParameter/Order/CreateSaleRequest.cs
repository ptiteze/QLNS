namespace QLNS_BackEnd.ModelsParameter.Order
{
    public class CreateSaleRequest
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Des { get; set; }

        public int AdminId { get; set; }
        public Dictionary<int, int>? Discount { get; set; } // productId -- discount percent
    }
}
