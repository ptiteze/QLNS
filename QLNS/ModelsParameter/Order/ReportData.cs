namespace QLNS.ModelsParameter.Order
{
    public class ReportData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LoaiGG { get; set; }
        public string Name { get; set; }
        public string Partner { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string ProductIn { get; set; } = "";
    }
}
