using Microsoft.EntityFrameworkCore;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Cart;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class CartRepository : ICart
    {
		public bool AddProduct(RequestAddCart request)
		{
			try
			{
				Product product = SingletonDataBridge.GetInstance().Products.Find(request.productId);
				Cart cart = new Cart()
				{
					ProductId = request.productId,
					Quantity = request.quantity,
					UserName = request.username,
					Product = product
				};
				SingletonDataBridge.GetInstance().Carts.Add(cart);
				SingletonDataBridge.GetInstance().SaveChanges();
				Console.WriteLine(cart.UserName + cart.ProductId + cart.Quantity);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("djasdjaskdj");
				return false;
			}
		}

		public bool CheckExistCart(RequestCheckCart request)
		{
			var cartCheck = SingletonDataBridge.GetInstance().Carts.Where(c => c.UserName == request.username && c.ProductId == request.productId).FirstOrDefault();
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
