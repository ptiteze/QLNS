using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Product;

namespace QLNS_BackEnd.Interfaces
{
    public interface IUsed
    {
        List<UsedDTO> GetAllUseds();
        UsedDTO GetUsedById(int id);
        List<UsedDTO> GetUsedsByProduct(int id);
        bool CreateUsedProduct(UsedProductRequest request);
        bool DeleteUsedProduct(UsedProductRequest request);
        bool CreateUsed(string usedname);
        bool DeleteUsed(int id);
    }
}
