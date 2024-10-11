namespace QLNS_BackEnd.DTO
{
    public class AdminDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int? IdAccount { get; set; }
    }
}
