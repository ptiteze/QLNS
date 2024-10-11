using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class UserViewModel
    {
        public List<UserDTO> Users { get; set; }
        public List<AccountDTO> Accounts { get; set; }
    }
}
