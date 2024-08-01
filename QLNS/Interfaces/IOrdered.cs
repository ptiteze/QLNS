using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IOrdered
    {
        Task<List<OrderedDTO>> GetOrderedsByOrderId(int id);
    }
}
