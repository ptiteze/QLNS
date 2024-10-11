namespace QLNS_BackEnd.Models
{
    public class UsedProduct
    {
        public int Id { get; set; }

        public int UsedId { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual Used Used { get; set; } = null!;
    }
}
