namespace QLNS_BackEnd.ModelsParameter.Cart
{
    public class RequestRemoveCart
    {
        public string UserName { get; set; } = null!;

        public int ProductId { get; set; }
    }
}
