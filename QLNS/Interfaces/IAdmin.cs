using QLNS.DTO;
using QLNS.ModelsParameter;

namespace QLNS.Interfaces
{
    public interface IAdmin
    {
        InfoLogin Login(string username, string password);
        List<AdminDTO> GetAdmins();
        bool CheckExits(string username, string email, string phone);
        bool CreateAdmin(AddAdmin request);
        bool UpdateAdmin(UpdateAdmin request);
        bool DeleteAdmin(int id);
        bool UnLockAdmin(int id);
        AdminDTO GetAdmin(int id);  
    }
}
