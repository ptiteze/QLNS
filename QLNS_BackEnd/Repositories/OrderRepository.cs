using Azure.Core;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Order;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class OrderRepository : IOrder
    {
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
                SingletonDataBridge.GetInstance().RemoveRange(carts);
                SingletonDataBridge.GetInstance().SaveChanges();
                return order.Id;
            }catch
            {
                return 0;
            }
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                Order order = SingletonDataBridge.GetInstance().Orders.Find(id);
                order.Status = 3;
                SingletonDataBridge.GetInstance().SaveChanges();
                List<Ordered> ordereds = SingletonDataBridge.GetInstance().Ordereds.Where(o => o.OrderId == id).ToList();
                List<Product> products = SingletonDataBridge.GetInstance().Products.ToList();
                foreach (Ordered o in ordereds)
                {
                    Product pr = products.Where(p => p.Id == o.ProductId).FirstOrDefault();
                    pr.Quantity += o.Qty;
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

        public OrderDTO GetOrderById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<OrderDTO>(
                SingletonDataBridge.GetInstance().Orders.Find(id));
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
        public bool UpDateOrder(OrderDTO request)
        {
            try
            {
                Order order = SingletonDataBridge.GetInstance().Orders.Find(request.Id);
                order.Status = request.Status;
                Console.WriteLine(request.Id.ToString() +"-"+request.UserId);
                SingletonDataBridge.GetInstance().Orders.Update(order);
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
