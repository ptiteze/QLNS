using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Order;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class SaleRepository : ISale
    {
        public bool CreateSale(CreateSaleRequest request)
        {
            try
            {
                List<SaleDetail> saleDetails = new List<SaleDetail>();
                List<Product> products = new List<Product>();
                List<Product> prcha = SingletonDataBridge.GetInstance().Products.ToList();
                Sale sale = new Sale()
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Des = request.Des,
                    AdminId = request.AdminId,
                };
                SingletonDataBridge.GetInstance().Sales.Add(sale);
                SingletonDataBridge.GetInstance().SaveChanges();
                foreach (KeyValuePair<int, int> item in request.Discount)
                {
                    SaleDetail sd = new SaleDetail() 
                    {
                        SaleId = sale.Id,
                        ProductId = item.Key,   
                        Discount = item.Value
                    };
                    Product? product = prcha.FirstOrDefault(p => p.Id == item.Key);
                    product.Discount = item.Value;
                    products.Add(product);
                    saleDetails.Add(sd);    
                }
                SingletonDataBridge.GetInstance().SaleDetails.AddRange(saleDetails);
                SingletonDataBridge.GetInstance().SaveChanges();
                SingletonDataBridge.GetInstance().Products.UpdateRange(products);
                SingletonDataBridge.GetInstance().SaveChanges();

                return true;
            }catch (Exception ex)
            {
                return false;
            }
            
        }

        public SaleDTO GetSaleById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<SaleDTO>(
                SingletonDataBridge.GetInstance().Sales.Find(id));
        }

        public List<SaleDetailDTO> GetSaleDetailById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<SaleDetailDTO>>(
                SingletonDataBridge.GetInstance().SaleDetails.Where(s=>s.SaleId==id).ToList());
        }

        public List<SaleDTO> GetSales()
        {
            return SingletonAutoMapper.GetInstance().Map<List<SaleDTO>>(
               SingletonDataBridge.GetInstance().Sales.ToList());
        }
    }
}
