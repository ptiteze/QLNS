namespace QLNS.ModelsParameter.Cart
{
    public class UpdateListCartRequest
    {
        public List<CartItemUpdateModel> Items { get; set; }
    }
    public class CartItemUpdateModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
