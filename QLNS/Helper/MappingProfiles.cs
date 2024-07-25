using AutoMapper;
using QLNS.DTO;
using QLNS.Models;
using QLNS.ModelsParameter;

namespace QLNS.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
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
            //Ordered
            CreateMap<Ordered, OrderedDTO>();
            //Producer
            CreateMap<Producer, ProducerDTO>();
            //Product
            CreateMap<Product, ProductDTO>();
            //ProductPrice
            CreateMap<ProductPrice, ProductPriceDTO>();
            //SubppyInvoice
            CreateMap<SupplyInvoice, SubpplyInvoiceDTO>();
            //Transaction
            CreateMap<Transaction, TransactionDTO>();
            //User
            CreateMap<User, UserDTO>();
        }
    }
}
