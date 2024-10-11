using System.Data;

namespace QLNS.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        /// <summary>
        /// 1-Đang hoạt đông; 0-Đã bị khóa
        /// </summary>
        public bool Status { get; set; }

        public int IdRole { get; set; }

        public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

        public virtual Role IdRoleNavigation { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
