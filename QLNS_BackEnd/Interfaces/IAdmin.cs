using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.DTO;

namespace QLNS_BackEnd.Interfaces
{
    public interface IAdmin
    {
		List<AdminDTO> GetAdmins();
		bool CheckExits(RequestCheckAdmin request);
		bool CreateAdmin(AddAdmin request);
        bool UpdateAdmin(UpdateAdmin request);
        bool DeleteAdmin(int id);
        AdminDTO GetAdmin(int id);  
    }
}
