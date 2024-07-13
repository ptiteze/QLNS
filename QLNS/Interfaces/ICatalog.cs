using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface ICatalog
    {
        CatalogDTO GetCatalogById(int id);
        List<CatalogDTO> GetAllCatalog();
    }
}
