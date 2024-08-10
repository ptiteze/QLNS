namespace QLNS_BackEnd.DTO
{
    public class SupplyListDTO
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int ProductId { get; set; }
        public int ImportPrice { get; set; }

    }
}
