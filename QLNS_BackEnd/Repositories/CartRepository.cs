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
		public async Task<bool> AddProduct(RequestAddCart request)
		{
			try
			{
				Product product = await SingletonDataBridge.GetInstance().Products.FindAsync(request.productId);
				
				RequestCheckCart check = new RequestCheckCart()
				{
					productId = request.productId,
					userId = request.userId,
				};
				if (await CheckExistCart(check))
				{
					Cart cart = await SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == request.userId && c.ProductId == request.productId).FirstOrDefaultAsync();
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
                 await SingletonDataBridge.GetInstance().SaveChangesAsync();
                return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> CheckExistCart(RequestCheckCart request)
		{
			var cartCheck = await SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == request.userId && c.ProductId == request.productId).FirstOrDefaultAsync();
			if (cartCheck != null) return true;
			return false;
		}

		public async Task<List<CartDTO>> GetCartsByUserId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<CartDTO>>(
                await SingletonDataBridge.GetInstance().Carts.Where(c => c.UserId == id).ToListAsync());
        }

        public async Task<bool> RemoveCart(RequestRemoveCart request)
        {
			try
			{
				Cart cart = await SingletonDataBridge.GetInstance().Carts.Where(c=>c.ProductId==request.ProductId && c.UserId ==request.UserId).FirstOrDefaultAsync();	
				SingletonDataBridge.GetInstance().Carts.Remove(cart);
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
