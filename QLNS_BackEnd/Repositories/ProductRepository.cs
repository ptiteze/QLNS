using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
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
