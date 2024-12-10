using Azure.Core;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Order;
using QLNS_BackEnd.Singleton;
using System.Data;
using Microsoft.Data.SqlClient;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace QLNS_BackEnd.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int CreateOrder(CreateOrderRequest request)
        {
            try
            {
                Order order = SingletonAutoMapper.GetInstance().Map<Order>(request);
                SingletonDataBridge.GetInstance().Orders.Add(order);
                SingletonDataBridge.GetInstance().SaveChanges();
                List<Cart> carts = SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == request.UserId).ToList();
                List<Product> products = SingletonDataBridge.GetInstance().Products.ToList();

                foreach (Cart c in carts)
                {
                    Product pr = products.Where(p => p.Id == c.ProductId).FirstOrDefault();
                    List<ImportDetail> ips = SingletonDataBridge.GetInstance().ImportDetails.Where(
                        i => i.ProductId == pr.Id && i.Status == true).ToList();
                    ips = ips.OrderByDescending(i => (i.QuantityImport ?? 0) - (i.Stock ?? 0)).ToList();
                    Ordered ordered = new Ordered()
                    {
                        OrderId = order.Id,
                        Order = order,
                        Price = pr.Price - (pr.Price * pr.Discount / 100),
                        ProductId = c.ProductId,
                        Product = pr,
                        Qty = c.Quantity,
                    };
                    SingletonDataBridge.GetInstance().Ordereds.Add(ordered);
                    SingletonDataBridge.GetInstance().SaveChanges();
                    pr.Quantity -= c.Quantity;
                    int qty = c.Quantity;
                    foreach (ImportDetail ip in ips)
                    {
                        if (ip.Stock >= qty)
                        {
                            ip.Stock -= qty;
                            if (ip.Stock == 0)
                            {
                                ip.Status = false;
                            }
                            break;
                        }
                        else
                        {
                            qty -= ip.Stock ?? 0;
                            ip.Stock = 0;
                            ip.Status = false;
                        }
                    }
                    SingletonDataBridge.GetInstance().ImportDetails.UpdateRange(ips);
                    SingletonDataBridge.GetInstance().Products.Update(pr);
                    SingletonDataBridge.GetInstance().SaveChanges();
                }
                bool check = SingletonDataBridge.GetInstance().Ordereds.Any(o=>o.OrderId==order.Id);
                if (!check)
                {
                    SingletonDataBridge.GetInstance().Orders.Remove(order);
                    SingletonDataBridge.GetInstance().SaveChanges();
                    return 0;
                }
                SingletonDataBridge.GetInstance().RemoveRange(carts);
                SingletonDataBridge.GetInstance().SaveChanges();
                return order.Id;
            }catch
            {
                return 0;
            }
        }

        public async Task<List<ReportData>> DataOrder(DateTime startDate, DateTime endDate)
        {
            List<ReportData> reports = new List<ReportData>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("V_BCDT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Truyền tham số
                        cmd.Parameters.AddWithValue("@bd", startDate);
                        cmd.Parameters.AddWithValue("@kt", endDate);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                reports.Add(new ReportData
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Date = Convert.ToDateTime(reader["date"]),
                                    LoaiGG = reader["LoaiGG"].ToString(),
                                    Name = reader["name"].ToString(),
                                    Partner = reader["patner"].ToString(),
                                    Status = reader["status"].ToString(),
                                    Amount = Convert.ToDecimal(reader["amount"])
                                });
                            }
                        }
                    }
                }
                return reports;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                Order order = SingletonDataBridge.GetInstance().Orders.Find(id);
                if(order == null || order.Status == 2 || order.Status == 3)
                {
                    return false;
                }
                order.Status = 3;
                SingletonDataBridge.GetInstance().SaveChanges();
                List<Ordered> ordereds = SingletonDataBridge.GetInstance().Ordereds.Where(o => o.OrderId == id).ToList();
                List<Product> products = SingletonDataBridge.GetInstance().Products.ToList();
                foreach (Ordered o in ordereds)
                {
                    Product pr = products.Where(p => p.Id == o.ProductId).FirstOrDefault();
                    
                    List<ImportDetail> list_IDT = SingletonDataBridge.GetInstance().ImportDetails.Where(
                        i => i.ProductId == pr.Id && i.Status == true && i.QuantityImport-i.Stock!=0).ToList();
                    list_IDT = list_IDT.OrderByDescending(i => (i.QuantityImport ?? 0) - (i.Stock ?? 0)).ToList();
                    int sumBack = 0;
                    foreach(ImportDetail pd in list_IDT)
                    {
                        if (o.Qty - sumBack + pd.Stock > pd.QuantityImport)
                        {
                            sumBack += (pd.QuantityImport??0 - pd.Stock??0);
                            pd.Stock = pd.QuantityImport;
                            SingletonDataBridge.GetInstance().ImportDetails.Update(pd);
                            SingletonDataBridge.GetInstance().SaveChanges();
                        }
                        else
                        {
                            sumBack = o.Qty;
                            pd.Stock += (o.Qty - sumBack);
                            SingletonDataBridge.GetInstance().ImportDetails.Update(pd);
                            SingletonDataBridge.GetInstance().SaveChanges();
                        }
                        if (sumBack == o.Qty) break;
                    }

                    pr.Quantity += sumBack;
                    SingletonDataBridge.GetInstance().Products.Update(pr);
                    SingletonDataBridge.GetInstance().SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<OrderDTO>(
                await SingletonDataBridge.GetInstance().Orders.FindAsync(id));
        }

        public List<OrderDTO> GetOrders()
        {
            return SingletonAutoMapper.GetInstance().Map<List<OrderDTO>>(
                SingletonDataBridge.GetInstance().Orders.ToList());
        }

        public List<OrderDTO> GetOrdersByUserId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<OrderDTO>>(
                SingletonDataBridge.GetInstance().Orders.Where(o => o.UserId == id).ToList());
        }
        public async Task<bool> UpDateOrder(OrderDTO request)
        {
            try
            {
                Order order = await SingletonDataBridge.GetInstance().Orders.FindAsync(request.Id);
                order.Payment = request.Payment;
                order.Status = request.Status;
                order.AdminId = request.AdminId;
                Console.WriteLine(request.Id.ToString() +"-"+request.UserId);
                SingletonDataBridge.GetInstance().Orders.Update(order);
                await SingletonDataBridge.GetInstance().SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
