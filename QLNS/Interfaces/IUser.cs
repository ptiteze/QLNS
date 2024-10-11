using QLNS.DTO;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.User;

namespace QLNS.Interfaces
{
    public interface IUser
    {
		Task<List<UserDTO>> GetUsers();
        Task<bool> CreateUser(AddUser request);
        Task<bool> UpdateUser(UserDTO request);
        Task<UserDTO> GetUserById(int id);
    }
}
