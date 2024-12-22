namespace QLNS_BackEnd.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Des { get; set; }

        public int AdminId { get; set; }
    }
}
