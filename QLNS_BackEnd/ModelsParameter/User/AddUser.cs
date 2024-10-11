namespace QLNS_BackEnd.ModelsParameter.User
{
    public class AddUser
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Nameuser { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public DateOnly Created { get; set; }
    }
}
