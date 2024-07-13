using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IProduct
    {
        ProductDTO GetProductById(int id);
        List<ProductDTO> GetAllProducts();
        List<ProductDTO> GetProductsByCatalogId(int id);
    }
}
