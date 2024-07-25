using QLNS.Data;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
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

        public bool AddCatalog(string name)
        {
            try
            {
                Catalog catalog = new Catalog();
                catalog.Name = name;
                SingletonDataBridge.GetInstance().Catalogs.Add(catalog);    
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           

        }

        public bool DeleteCatalog(int id)
        {
            try
            {
                Catalog catalog = SingletonDataBridge.GetInstance().Catalogs.Find(id);
                SingletonDataBridge.GetInstance().Catalogs.Remove(catalog);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
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

        public bool UpdateCatalog(int id, string name)
        {
			try
			{
				Catalog catalog = SingletonDataBridge.GetInstance().Catalogs.Find(id);
				catalog.Name = name;
				SingletonDataBridge.GetInstance().Catalogs.Update(catalog);
				SingletonDataBridge.GetInstance().SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}
    }
}
