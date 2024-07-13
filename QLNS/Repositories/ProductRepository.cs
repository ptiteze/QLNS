using QLNS.Data;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly DataContext _dataContext;
        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public List<ProductDTO> GetAllProducts()
        {
            return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(
                _dataContext.Products.Where(p => p.Status == 1).ToList());
        }

        public ProductDTO GetProductById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<ProductDTO>(
                _dataContext.Products.Where(p => p.Status == 1 && p.Id == id).ToList());
        }

        public List<ProductDTO> GetProductsByCatalogId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(
                _dataContext.Products.Where(p => p.Status == 1 && p.CatalogId == id).ToList());
        }
    }
}
