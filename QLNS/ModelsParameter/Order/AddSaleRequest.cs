namespace QLNS.ModelsParameter.Order
{
    public class AddSaleRequest
    {
        public DateOnly start_date {get; set;}
        public DateOnly end_date {get; set;}
        public string desc {get; set;}
        public int admin_id {get; set;}
        public string discountData { get; set; } = "";
    }
}
