using AutoMapper;
using QLNS.DTO;
using QLNS.Models;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.Order;
using QLNS.ModelsParameter.Product;
using QLNS.ModelsParameter.SupplyInvoice;
using QLNS.ModelsParameter.SupplyList;

namespace QLNS.Helper
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
            // Catalog
            CreateMap<Catalog, CatalogDTO>();
            //ImportDetail
            CreateMap<ImportDetail, ImportDetailDTO>();
            //Order
            CreateMap<Order, OrderDTO>();
            CreateMap<CreateOrderRequest, Order>();
            //Ordered
            CreateMap<Ordered, OrderedDTO>();
            //Producer
            CreateMap<Producer, ProducerDTO>();
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
        }
    }
}
