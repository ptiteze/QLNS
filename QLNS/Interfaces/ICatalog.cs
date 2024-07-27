using QLNS.DTO;
using QLNS.ModelsParameter.Catalog;

namespace QLNS.Interfaces
{
    public interface ICatalog
    {
        Task<CatalogDTO> GetCatalogById(int id);
        Task<List<CatalogDTO>> GetAllCatalog();
		Task<bool> AddCatalog(string name);
		Task<bool> UpdateCatalog(UpdateCatalogRequest request);
		Task<bool> DeleteCatalog(int id);
    }
}
