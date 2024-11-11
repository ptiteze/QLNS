using QLNS_BackEnd.Models;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Product;
using QLNS_BackEnd.Singleton;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;

namespace QLNS_BackEnd.Repositories
{
    public class UsedRepository : IUsed
    {
        public bool CreateUsed(string usedname)
        {
            try
            {
                bool check = SingletonDataBridge.GetInstance().Useds.Any(x => x.Name == usedname);
                if (check) return false;
                Used used = new Used() { Name = usedname };
                SingletonDataBridge.GetInstance().Useds.Add(used);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateUsedProduct(UsedProductRequest request)
        {
            try
            {
                bool check = SingletonDataBridge.GetInstance().UsedProducts.Any(x => x.UsedId == request.UsedId && x.ProductId == request.ProductId);
                if (check) return false;
                UsedProduct usedPr = new UsedProduct() { ProductId = request.ProductId, UsedId = request.UsedId };
                SingletonDataBridge.GetInstance().UsedProducts.Add(usedPr);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUsed(int id)
        {
            try
            {
                Used used = SingletonDataBridge.GetInstance().Useds.Find(id);
                if (used == null) return false;
                bool check2 = SingletonDataBridge.GetInstance().UsedProducts.Any(x => x.UsedId == id);
                if (check2) return false;
                SingletonDataBridge.GetInstance().Useds.Remove(used);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteUsedProduct(UsedProductRequest request)
        {
            try 
            {
                UsedProduct up = SingletonDataBridge.GetInstance().UsedProducts.Where(x => x.UsedId == request.UsedId && x.ProductId == request.ProductId).FirstOrDefault();
                if(up==null) return false;
                SingletonDataBridge.GetInstance().UsedProducts.Remove(up);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public List<UsedDTO> GetAllUseds()
        {
            return SingletonAutoMapper.GetInstance().Map<List<UsedDTO>>(
                SingletonDataBridge.GetInstance().Useds.ToList());
        }

        public UsedDTO GetUsedById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<UsedDTO>(
                SingletonDataBridge.GetInstance().Useds.Find(id));
        }

        public List<UsedDTO> GetUsedsByProduct(int id)
        {
            List<UsedProduct> up = SingletonDataBridge.GetInstance().UsedProducts.Where(x => x.ProductId == id).ToList();
            List<int> ints = up.Select(x=> x.UsedId).ToList();
            if (ints.IsNullOrEmpty()) return null;
            List<UsedDTO> useds = new List<UsedDTO>();
            foreach(int i in ints)
            {
                useds.Add(GetUsedById(i));
            }
            return useds;
        }
    }
}
