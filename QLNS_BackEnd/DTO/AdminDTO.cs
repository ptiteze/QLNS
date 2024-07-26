namespace QLNS_BackEnd.DTO
{
    public class AdminDTO
    {
        public int Id { get; set; } = 0;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
        public int? Status { get; set; }
        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
