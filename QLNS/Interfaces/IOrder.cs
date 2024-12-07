using QLNS.DTO;
using QLNS.ModelsParameter.Order;

namespace QLNS.Interfaces
{
    public interface IOrder
    {
        Task<List<OrderDTO>> GetOrdersByUserId(int id);
        Task<OrderDTO> GetOrderById(int id);
        Task<int> CreateOrder(CreateOrderRequest request);
        Task<bool> UpDateOrder(OrderDTO request);
        Task<bool> DeleteOrder(int id);
        Task<List<OrderDTO>> GetOrders();
    }
}
