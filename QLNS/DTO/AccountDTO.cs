namespace QLNS.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        /// <summary>
        /// 1-Đang hoạt đông; 0-Đã bị khóa
        /// </summary>
        public bool Status { get; set; }

        public int IdRole { get; set; }
    }
}
