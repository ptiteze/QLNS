using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.DTO;

namespace QLNS_BackEnd.Interfaces
{
    public interface IAdmin
    {
		InfoLogin Login(RequestLogin request);
		List<AdminDTO> GetAdmins();
		bool CheckExits(RequestCheckAdmin request);
		bool CreateAdmin(AddAdmin request);
        bool UpdateAdmin(UpdateAdmin request);
        bool DeleteAdmin(int id);
        bool UnLockAdmin(int id);
        AdminDTO GetAdmin(int id);  
    }
}
