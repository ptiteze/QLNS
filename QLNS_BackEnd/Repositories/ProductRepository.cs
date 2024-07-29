using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Product;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class ProductRepository : IProduct
    {
		public bool AddProduct(InputProductRequest request)
		{
            try
            {
                Product product = SingletonAutoMapper.GetInstance().Map<Product>(request);
                product.Quantity = 0;
                SingletonDataBridge.GetInstance().Products.Add(product);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
		}

		public bool DeleteProduct(int id)
		{
            try
            {
                if (SingletonDataBridge.GetInstance().Ordereds.Any(o => o.ProductId == id))
                    return false;
                
                Product product = SingletonDataBridge.GetInstance().Products.Find(id);
                if(product.Quantity != 0) return false;
                SingletonDataBridge.GetInstance().Products.Remove(product);
                SingletonDataBridge.GetInstance().SaveChanges();    
				return true;
            }
            catch
            {
                return false;
            }
		}

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

		public bool UpdateProduct(UpdateProductRequest request)
		{
            try
            {
                Product product = SingletonDataBridge.GetInstance().Products.Find(request.Id);
                product.Name = request.Name;
                product.Description = request.Description;  
                product.Status = request.Status;
                product.CatalogId = request.CatalogId; 
                product.Discount = request.Discount;
                product.Price = request.Price;
                product.Content = request.Content;
                product.ImageLink = request.ImageLink;
                product.ExpiryDate = request.ExpiryDate;
                product.Unit = request.Unit;
                SingletonDataBridge.GetInstance().Products.Update(product);
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
