using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetOrdersByUsername(string username);
        OrderDTO GetOrderById(int id);
        int CreateOrder(CreateOrderRequest request);
        bool UpDateOrder(OrderDTO request);
    }
}
