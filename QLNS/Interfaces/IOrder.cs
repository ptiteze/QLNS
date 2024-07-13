using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetOrdersByUsername(string username);
        OrderDTO GetOrderById(int id);

    }
}
