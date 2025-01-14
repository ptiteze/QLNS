﻿using QLNS.DTO;
using QLNS.ModelsParameter.Cart;

namespace QLNS.Interfaces
{
    public interface ICart
    {
        Task<List<CartDTO>?> GetCartsByUserId(int id);
		Task<bool> AddProduct(RequestAddCart request);
		Task<Boolean> CheckExistCart(RequestCheckCart request);
        Task<Boolean> RemoveCart(RequestRemoveCart request);
    }
}
