using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class OrderedRepository : IOrdered
    {
        public List<OrderedDTO> GetOrderedsByOrderId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<OrderedDTO>>(
                SingletonDataBridge.GetInstance().Ordereds.Where(o=>o.OrderId==id).ToList());
        }
    }
}
