namespace QLNS_BackEnd.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Nameuser { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public DateOnly Created { get; set; }

        public int? IdAccount { get; set; }
    }
}
