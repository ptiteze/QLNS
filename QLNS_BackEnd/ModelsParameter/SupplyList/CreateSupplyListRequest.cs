namespace QLNS_BackEnd.ModelsParameter.SupplyList
{
    public class CreateSupplyListRequest
    {
        public int? Quantity { get; set; }

        public int ProductId { get; set; }
        public int ImportPrice { get; set; } = 0;
    }
}
