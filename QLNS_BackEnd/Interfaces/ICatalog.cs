using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Catalog;

namespace QLNS_BackEnd.Interfaces
{
    public interface ICatalog
    {
        CatalogDTO GetCatalogById(int id);
        Task<List<CatalogDTO>> GetAllCatalog();
        bool AddCatalog(string name);
        bool UpdateCatalog(UpdateCatalogRequest request);
        bool DeleteCatalog(int id);
    }
}
