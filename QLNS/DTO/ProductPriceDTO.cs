namespace QLNS.DTO
{
    public class ProductPriceDTO
    {
        public int ProductId { get; set; }

        public DateOnly AppliedDate { get; set; }

        public int Price { get; set; }

        public int AdminId { get; set; } = 0;
    }
}
