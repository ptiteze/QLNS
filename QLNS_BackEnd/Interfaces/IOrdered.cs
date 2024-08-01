using QLNS_BackEnd.DTO;

namespace QLNS_BackEnd.Interfaces
{
    public interface IOrdered
    {
        List<OrderedDTO> GetOrderedsByOrderId(int id);
    }
}
