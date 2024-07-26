using QLNS.DTO;
using QLNS.ModelsParameter.Admin;

namespace QLNS.Interfaces
{
    public interface IUser
    {
        InfoLogin Login(RequestLogin request);

        List<UserDTO> GetUsers();
        bool LockUser(string username);
        bool UnLockUser(string username);
    }
}
