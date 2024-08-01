using QLNS.DTO;
using QLNS.ModelsParameter.Order;

namespace QLNS.Interfaces
{
    public interface IOrder
    {
        Task<List<OrderDTO>> GetOrdersByUsername(string username);
        Task<OrderDTO> GetOrderById(int id);
        Task<int> CreateOrder(CreateOrderRequest request);
        Task<bool> UpDateOrder(OrderDTO request);

    }
}
