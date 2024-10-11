using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.ModelsParameter.User;

namespace QLNS_BackEnd.Interfaces
{
    public interface IUser
    {
        List<UserDTO> GetUsers();
        bool CreateUser(AddUser request);
        bool UpdateUser(UpdateUser request);
        UserDTO GetUserById(int id);

    }
}
