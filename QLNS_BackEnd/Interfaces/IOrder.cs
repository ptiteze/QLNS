using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetOrdersByUserId(int id);
        Task<OrderDTO> GetOrderById(int id);
        int CreateOrder(CreateOrderRequest request);
        Task<bool> UpDateOrder(OrderDTO request);
        List<OrderDTO> GetOrders();
        bool DeleteOrder(int id);
    }
}
