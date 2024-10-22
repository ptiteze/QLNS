using QLNS.DTO;
using QLNS.ModelsParameter.Product;

namespace QLNS.Interfaces
{
    public interface IProduct
    {
        Task<ProductDTO> GetProductById(int id);
		Task<List<ProductDTO>> GetAllProducts();
        Task<List<ProductDTO>> GetProductsByCatalogId(int id);
        Task<bool> CreateProduct(InputProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(int id);
        Task<List<ProductDTO>> GetRecommendedProducts(int id);
        Task<List<ProductDTO>> GetBestSellingProducts();
    }
}
