namespace QLNS_BackEnd.DTO
{
    public class CartDTO
    {
        public string UserName { get; set; } = null!;

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
