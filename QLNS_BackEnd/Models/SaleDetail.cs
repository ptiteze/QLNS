namespace QLNS_BackEnd.Models
{
    public class SaleDetail
    {
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int Discount { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual Sale Sale { get; set; } = null!;
    }
}
