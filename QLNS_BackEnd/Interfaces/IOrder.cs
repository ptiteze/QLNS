using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetOrdersByUserId(int id);
        OrderDTO GetOrderById(int id);
        int CreateOrder(CreateOrderRequest request);
        bool UpDateOrder(OrderDTO request);
        List<OrderDTO> GetOrders();
        bool DeleteOrder(int id);
    }
}
