using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class AdminRepository : IAdmin
    {
        private readonly IAccount Iacc;

        public AdminRepository(IAccount accountRepository)
        {
            Iacc = accountRepository;
        }
        public bool CheckExits(RequestCheckAdmin request)
		{
			Admin admin = SingletonDataBridge.GetInstance().Admins.Where(a => a.Email == request.email || a.Phone == request.phone).FirstOrDefault();
			if (admin != null) return true;
			return false;
		}

		public bool CreateAdmin(AddAdmin request)
        {
            try
            {
                bool check = SingletonDataBridge.GetInstance().Admins.Any(a => a.Email == request.Email || a.Phone == request.Phone);
                if (check) return false;
                AccountRepository accountRepository;
                Admin admin = new Admin()
                {
                    Email = request.Email,
                    Phone = request.Phone,
                    Name = request.Name,
                };
                RequestLogin requestLogin = new RequestLogin()
                {
                    username = request.Username,
                    password = request.Password,
                };
                int idAccount = Iacc.CreateLogin(requestLogin, request.Role);
                if (idAccount == 0) return false;
                admin.IdAccount = idAccount;
                SingletonDataBridge.GetInstance().Admins.Add(admin);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool DeleteAdmin(int id)
        {
            try
            {
                Admin admin = SingletonDataBridge.GetInstance().Admins.Find(id);
                Order order = SingletonDataBridge.GetInstance().Orders.Where(e => e.AdminId == id).FirstOrDefault();
                SupplyInvoice si = SingletonDataBridge.GetInstance().SupplyInvoices.Where(e => e.AdId == id).FirstOrDefault();
                if (order == null && si == null)
                {
                    if(!Iacc.LockAccount(admin.IdAccount)) return false;
                    
                    SingletonDataBridge.GetInstance().Admins.Remove(admin);
                    SingletonDataBridge.GetInstance().SaveChanges();
                    return true;
                }
                else
                {
                    return Iacc.LockAccount(admin.IdAccount);
                }
                
            }
            catch {
                return false;
            }
            
        }

        public AdminDTO GetAdmin(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<AdminDTO>(SingletonDataBridge.GetInstance().Admins.Find(id));
        }

        public List<AdminDTO> GetAdmins()
        {
           return SingletonAutoMapper.GetInstance().Map<List<AdminDTO>>(
                SingletonDataBridge.GetInstance().Admins.ToList());
        }

        public bool UpdateAdmin(UpdateAdmin request)
        {
            try
            {
                bool check = SingletonDataBridge.GetInstance().Admins.Any(a => (a.Email == request.Email || a.Phone == request.Phone) && a.Id!=request.Id);
                if (check) return false;
                Admin admin = SingletonDataBridge.GetInstance().Admins.Find(request.Id);
                admin.Name = request.Name;
                admin.Phone = request.Phone;
                admin.Email = request.Email;
                if (!Iacc.ChangePassword(request.Password, admin.IdAccount)) return false;
                SingletonDataBridge.GetInstance().Admins.Update(admin);
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
