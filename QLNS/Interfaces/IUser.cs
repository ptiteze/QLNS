using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IUser
    {
        InfoLogin Login(string username, string password);

        List<UserDTO> GetUsers();
    }
}
