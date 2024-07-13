using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IAdmin
    {
        InfoLogin Login(string username, string password);

    }
}
