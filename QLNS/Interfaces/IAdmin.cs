using QLNS.DTO;
using QLNS.ModelsParameter.Admin;

namespace QLNS.Interfaces
{
    public interface IAdmin
    {
        Task<List<AdminDTO>> GetAdmins();
		Task<bool> CheckExits(RequestCheckAdmin request);
        Task<bool> CreateAdmin(AddAdmin request);
        Task<bool> UpdateAdmin(UpdateAdmin request);
        Task<bool> DeleteAdmin(int id);
        Task<AdminDTO> GetAdmin(int id);  
    }
}
