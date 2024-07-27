using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IProduct
    {
        Task<ProductDTO> GetProductById(int id);
		Task<List<ProductDTO>> GetAllProducts();
        Task<List<ProductDTO>> GetProductsByCatalogId(int id);
    }
}
