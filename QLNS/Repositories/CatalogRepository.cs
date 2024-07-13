using QLNS.Data;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class CatalogRepository : ICatalog
    {
        private readonly DataContext _dataContext;
        public CatalogRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public List<CatalogDTO> GetAllCatalog()
        {
            return SingletonAutoMapper.GetInstance().Map<List<CatalogDTO>>(_dataContext.Catalogs.ToList());
        }

        public CatalogDTO GetCatalogById(int id)
        {
            return SingletonAutoMapper.GetInstance()
                .Map<CatalogDTO>(_dataContext.Catalogs.Where(c => c.Id == id).FirstOrDefault());
        }
    }
}
