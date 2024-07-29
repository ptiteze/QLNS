using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.SupplyList;

namespace QLNS_BackEnd.Interfaces
{
    public interface ISupplyList
    {
        List<SupplyListDTO> GetAllSupplyList();
        bool CreateSupplyList(CreateSupplyListRequest request);
        bool DeleteSupplyList(int id);
        bool UpdateSupplyList(CreateSupplyListRequest request); // update Quantity
    }
}
