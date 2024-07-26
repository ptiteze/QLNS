using QLNS_BackEnd.DTO;

namespace QLNS_BackEnd.Interfaces
{
    public interface IProduct
    {
        ProductDTO GetProductById(int id);
        List<ProductDTO> GetAllProducts();
        List<ProductDTO> GetProductsByCatalogId(int id);
    }
}
