﻿using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Cart;
using QLNS_BackEnd.ModelsParameter.Product;

namespace QLNS_BackEnd.Interfaces
{
    public interface IProduct
    {
        ProductDTO GetProductById(int id);
        Task<List<ProductDTO>> GetAllProducts();
        List<ProductDTO> GetProductsByCatalogId(int id);
        bool AddProduct(InputProductRequest request);
        bool UpdateProduct(UpdateProductRequest request);
        bool DeleteProduct(int id);
        List<ProductDTO> GetRecommendedProducts(int id);
        Task<List<ProductDTO>> GetProductsBestSelling();
        bool CheckPurchase(RequestCheckCart request);
        List<ProductDTO> GetRecommendedProductsByRated(int id);

	}
}
