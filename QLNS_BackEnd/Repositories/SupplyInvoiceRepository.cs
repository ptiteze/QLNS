using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.SupplyInvoice;
using QLNS_BackEnd.Singleton;
using QLNS_BackEnd.Models;
using Microsoft.IdentityModel.Tokens;
using DevExpress.Office.Utils;

namespace QLNS_BackEnd.Repositories
{
    public class SupplyInvoiceRepository : ISupplyInvoice
    {
        public bool CreateSupplyInvoice(CreateSupplyInvoiceRequest request)
        {
            try
            {
                List<SupplyList> supplyLists = new List<SupplyList>(SingletonDataBridge.GetInstance().SupplyLists);
                if (supplyLists.IsNullOrEmpty()) return false;
                Admin ad = SingletonDataBridge.GetInstance().Admins.Find(request.AdId);
                Producer pru = SingletonDataBridge.GetInstance().Producers.Find(request.ProducerId);
                SupplyInvoice si = new SupplyInvoice
                { //SingletonAutoMapper.GetInstance().Map<SupplyInvoice>(request);
                    AdId = request.AdId,
                    ProducerId = request.ProducerId,
                    SupplyTime = request.SupplyTime,
                    Ad = ad,
                    Producer = pru,
                };
                SingletonDataBridge.GetInstance().SupplyInvoices.Add(si);
                SingletonDataBridge.GetInstance().SaveChanges();
                
                List<Product> products = SingletonDataBridge.GetInstance().Products.ToList();
                foreach (SupplyList supply in supplyLists)
                {
                    Product pr = products.Where(p => p.Id == supply.ProductId).FirstOrDefault();
                    ImportDetail import = new ImportDetail()
                    {
                        InvoiceId = si.Id,
                        ProductId = supply.ProductId,
                        ImportPrice = supply.ImportPrice,
                        QuantityImport = supply.Quantity,
                        Stock = supply.Quantity,
                        Status = true,
                    };
                    SingletonDataBridge.GetInstance().ImportDetails.Add(import);
                    SingletonDataBridge.GetInstance().SaveChanges();
                    pr.Quantity += supply.Quantity;
                    SingletonDataBridge.GetInstance().Products.Update(pr);
                    SingletonDataBridge.GetInstance().SaveChanges();
                }
                var dataRemove = SingletonDataBridge.GetInstance().SupplyLists.ToList();
                SingletonDataBridge.GetInstance().SupplyLists.RemoveRange(dataRemove);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SupplyInvoiceDTO> GetAllSupplyInvoice()
        {
            return SingletonAutoMapper.GetInstance().Map<List<SupplyInvoiceDTO>>(
                SingletonDataBridge.GetInstance().SupplyInvoices.ToList());
        }

        public SupplyInvoiceDTO GetSupplyInvoiceById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<SupplyInvoiceDTO>(
                SingletonDataBridge.GetInstance().SupplyInvoices.Find(id));
        }

        public List<ViewSupply> ViewSupplies()
        {
            List<ViewSupply> viewSupplies = new List<ViewSupply>();
            List<SupplyInvoice> list_supply = SingletonDataBridge.GetInstance().SupplyInvoices.ToList();
            List<ImportDetail> list_ip = SingletonDataBridge.GetInstance().ImportDetails.ToList();
            List<Product> list_pr = SingletonDataBridge.GetInstance().Products.ToList();
            foreach (SupplyInvoice si in list_supply)
            {
                ViewSupply view = new ViewSupply() 
                {
                    Id = si.Id,
                    AdId = si.AdId,
                    SupplyTime = si.SupplyTime,
                    ProducerId = si.ProducerId,
                };
                int sum = 0;
                string cataIn = "";
                string proIn = "";
                foreach (ImportDetail ip in list_ip.Where(i => i.InvoiceId == si.Id).ToList())
                {
                    sum += ip.ImportPrice;
                    Product pr = list_pr.Where(p=>p.Id == ip.ProductId).FirstOrDefault();
                    cataIn += (" " + pr.CatalogId.ToString());
                    proIn += (" " + pr.Id.ToString());
                }
                view.Amount = sum;
                view.ProductIn = proIn;
                view.CatalogIn = cataIn;
                viewSupplies.Add(view);
            }
            return viewSupplies;
        }
    }
}