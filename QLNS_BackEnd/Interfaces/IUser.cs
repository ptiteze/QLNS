using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Admin;

namespace QLNS_BackEnd.Interfaces
{
    public interface IUser
    {
        InfoLogin Login(RequestLogin request);

        List<UserDTO> GetUsers();
        bool LockUser(string username);
        bool UnLockUser(string username);
    }
}
