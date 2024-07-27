using QLNS.DTO;
using QLNS.ModelsParameter.Admin;

namespace QLNS.Interfaces
{
    public interface IUser
    {
        Task<InfoLogin> Login(RequestLogin request);
		Task<List<UserDTO>> GetUsers();
        Task<bool> LockUser(string username);
        Task<bool> UnLockUser(string username);
    }
}
