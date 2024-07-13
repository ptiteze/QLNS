namespace QLNS.DTO
{
    public class ProductPriceDTO
    {
        public int ProductId { get; set; }

        public DateOnly AppliedDate { get; set; }

        public int Price { get; set; }

        public string AdminId { get; set; } = null!;
    }
}
