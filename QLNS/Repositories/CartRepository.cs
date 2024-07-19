using Microsoft.EntityFrameworkCore;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class CartRepository : ICart
    {
        public bool AddProduct(string username, int productId, int quantity)
        {
            try {
                Product product = SingletonDataBridge.GetInstance().Products.Find(productId);
                Cart cart = new Cart()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserName = username,
                    Product = product
                };
                SingletonDataBridge.GetInstance().Carts.Add(cart);
                SingletonDataBridge.GetInstance().SaveChanges();
                Console.WriteLine(cart.UserName+cart.ProductId+cart.Quantity);
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine("djasdjaskdj");
                return false;
            }
        }

        public bool CheckExistCart(string username, int productId)
        {
            var cartCheck = SingletonDataBridge.GetInstance().Carts.Where(c => c.UserName == username && c.ProductId == productId).FirstOrDefault();
            if (cartCheck != null) return true;
            return false;
        }

        public List<CartDTO> GetCartsByUsername(string username)
        {
            return SingletonAutoMapper.GetInstance().Map<List<CartDTO>>(
                SingletonDataBridge.GetInstance().Carts.Where(c => c.UserName == username));
        }
    }
}
