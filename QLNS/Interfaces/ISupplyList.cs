using QLNS.DTO;
using QLNS.ModelsParameter.SupplyList;

namespace QLNS.Interfaces
{
    public interface ISupplyList
    {
        Task<List<SupplyListDTO>> GetAllSupplyList();
        Task<bool> CreateSupplyList(CreateSupplyListRequest request);
        Task<bool> DeleteSupplyList(int id);
        Task<bool> UpdateSupplyList(CreateSupplyListRequest request); // update Quantity
    }
}
