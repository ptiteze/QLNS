using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface ICatalog
    {
        CatalogDTO GetCatalogById(int id);
        List<CatalogDTO> GetAllCatalog();
        bool AddCatalog(string name);
        bool UpdateCatalog(int id, string name);
        bool DeleteCatalog(int id);
    }
}
