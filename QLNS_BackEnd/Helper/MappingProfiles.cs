using AutoMapper;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.ModelsParameter.Cart;
using QLNS_BackEnd.ModelsParameter.Order;
using QLNS_BackEnd.ModelsParameter.Producer;
using QLNS_BackEnd.ModelsParameter.Product;
using QLNS_BackEnd.ModelsParameter.SupplyInvoice;
using QLNS_BackEnd.ModelsParameter.SupplyList;

namespace QLNS_BackEnd.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            // Account
            CreateMap<Account, AccountDTO>();
            // Admin
            CreateMap<Admin, AdminDTO>();
            CreateMap<AddAdmin, Admin>();
            CreateMap<UpdateAdmin, Admin>();
            // Cart
            CreateMap<Cart, CartDTO>();
            CreateMap<RequestRemoveCart, Cart>();
            // Catalog
            CreateMap<Catalog, CatalogDTO>();
            //ImportDetail
            CreateMap<ImportDetail, ImportDetailDTO>();
            //Order
            CreateMap<Order, OrderDTO>();
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<OrderDTO, Order>();
            //Ordered
            CreateMap<Ordered, OrderedDTO>();
            //Producer
            CreateMap<Producer, ProducerDTO>();
            CreateMap<ProducerDTO, Producer>();
            CreateMap<CreateProducerRequest, Producer>();
			//Product
			CreateMap<Product, ProductDTO>();
            CreateMap<InputProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
            //SubppyInvoice
            CreateMap<SupplyInvoice, SupplyInvoiceDTO>();
            CreateMap<CreateSupplyInvoiceRequest, SupplyInvoice>();
            //SupplyList
            CreateMap<SupplyList, SupplyListDTO>();
            CreateMap<CreateSupplyListRequest, SupplyList>();
            //User
            CreateMap<User, UserDTO>();
            //Review
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();
            CreateMap<CreateReviewRequest, Review>();
            //Used
            CreateMap<Used, UsedDTO>();
            //Sale
            CreateMap<Sale, SaleDTO>();
            CreateMap<SaleDetail, SaleDetailDTO>();
        }
    }
}
