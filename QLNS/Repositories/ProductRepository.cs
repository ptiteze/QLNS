using QLNS.Data;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class ProductRepository : IProduct
    {
        public List<ProductDTO> GetAllProducts()
        {
            return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(
                SingletonDataBridge.GetInstance().Products.Where(p => p.Status == 1).ToList());
        }

        public ProductDTO GetProductById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<ProductDTO>(
                SingletonDataBridge.GetInstance().Products.Where(p => p.Status == 1 && p.Id == id).FirstOrDefault());
        }

        public List<ProductDTO> GetProductsByCatalogId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(
                SingletonDataBridge.GetInstance().Products.Where(p => p.Status == 1 && p.CatalogId == id).ToList());
        }
    }
}
