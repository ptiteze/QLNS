﻿using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.SupplyInvoice;
using QLNS_BackEnd.Singleton;
using QLNS_BackEnd.Models;

namespace QLNS_BackEnd.Repositories
{
    public class SupplyInvoiceRepository : ISupplyInvoice
    {
        public bool CreateSupplyInvoice(CreateSupplyInvoiceRequest request)
        {
            try
            {
                SupplyInvoice si = SingletonAutoMapper.GetInstance().Map<SupplyInvoice>(request);
                SingletonDataBridge.GetInstance().SupplyInvoices.Add(si);
                SingletonDataBridge.GetInstance().SaveChanges();
                List<SupplyList> supplyLists = new List<SupplyList>(SingletonDataBridge.GetInstance().SupplyLists);
                List<Product> products = SingletonDataBridge.GetInstance().Products.ToList();
                foreach(SupplyList supply in supplyLists)
                {
                    Product pr = products.Where(p => p.Id == supply.ProductId).FirstOrDefault();
                    ImportDetail import = new ImportDetail()
                    {
                        InvoiceId = si.Id,
                        ProductId = supply.ProductId,
                        ImportPrice = pr.Price,
                        QuantityImport = supply.Quantity,
                        Stock = supply.Quantity,
                        Status = true,
                        S= null,
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
    }
}