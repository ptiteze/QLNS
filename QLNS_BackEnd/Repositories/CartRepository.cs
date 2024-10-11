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
				
				RequestCheckCart check = new RequestCheckCart()
				{
					productId = request.productId,
					userId = request.userId,
				};
				if (CheckExistCart(check))
				{
					Cart cart = SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == request.userId && c.ProductId == request.productId).FirstOrDefault();
					cart.Quantity=request.quantity;
                    SingletonDataBridge.GetInstance().Carts.Update(cart);

                }
				else
				{
                    Cart cart = new Cart()
                    {
                        ProductId = request.productId,
                        Quantity = request.quantity,
                        UserId = request.userId,
                    };
                    SingletonDataBridge.GetInstance().Carts.Add(cart);
                }
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CheckExistCart(RequestCheckCart request)
		{
			var cartCheck = SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == request.userId && c.ProductId == request.productId).FirstOrDefault();
			if (cartCheck != null) return true;
			return false;
		}

		public List<CartDTO> GetCartsByUserId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<CartDTO>>(
                SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == id));
        }

        public bool RemoveCart(RequestRemoveCart request)
        {
			try
			{
				Cart cart = SingletonDataBridge.GetInstance().Carts.Where(c=>c.ProductId==request.ProductId && c.UserId ==request.UserId).FirstOrDefault();	
				SingletonDataBridge.GetInstance().Carts.Remove(cart);
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
