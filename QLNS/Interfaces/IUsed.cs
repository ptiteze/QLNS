using QLNS.DTO;
using QLNS.ModelsParameter.Product;

namespace QLNS.Interfaces
{
    public interface IUsed
    {
        Task<List<UsedDTO>> GetAllUseds();
        Task<UsedDTO> GetUsedById(int id);
        Task<List<UsedDTO>> GetUsedsByProduct(int id);
        Task<bool> CreateUsedProduct(UsedProductRequest request);
        Task<bool> DeleteUsedProduct(UsedProductRequest request);
        Task<bool> CreateUsed(string usedname);
        Task<bool> DeleteUsed(int id);
    }
}
