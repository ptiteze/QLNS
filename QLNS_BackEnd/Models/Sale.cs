namespace QLNS_BackEnd.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Des { get; set; }

        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; } = null!;

        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
