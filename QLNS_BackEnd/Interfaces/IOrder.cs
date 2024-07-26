using QLNS_BackEnd.DTO;

namespace QLNS_BackEnd.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetOrdersByUsername(string username);
        OrderDTO GetOrderById(int id);

    }
}
