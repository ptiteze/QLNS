using Azure.Core;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.SupplyList;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class SupplyListRepository : ISupplyList
    {
        public bool CreateSupplyList(CreateSupplyListRequest request)
        {
            try
            {
                if(SingletonDataBridge.GetInstance().SupplyLists.Any(s=>s.ProductId == request.ProductId)) return false;
                SupplyList sp = SingletonAutoMapper.GetInstance().Map<SupplyList>(request);
                SingletonDataBridge.GetInstance().SupplyLists.Add(sp);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool DeleteSupplyList(int id)
        {
            try
            {
                SupplyList sp = SingletonDataBridge.GetInstance().SupplyLists.Find(id);
                SingletonDataBridge.GetInstance().SupplyLists.Remove(sp);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SupplyListDTO> GetAllSupplyList()
        {
            return SingletonAutoMapper.GetInstance().Map<List<SupplyListDTO>>(
                SingletonDataBridge.GetInstance().SupplyLists);
        }

        public bool UpdateSupplyList(CreateSupplyListRequest request)
        { 
            try
            {
                SupplyList sp = SingletonDataBridge.GetInstance().SupplyLists.Where(s => s.ProductId == request.ProductId).FirstOrDefault();
                sp.Quantity = request.Quantity;
                SingletonDataBridge.GetInstance().SupplyLists.Update(sp);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
