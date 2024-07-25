namespace QLNS.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Nameuser { get; set; } = null!;
        public int? Status { get; set; }

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly Created { get; set; }
    }
}
