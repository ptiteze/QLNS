using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class AdminRepository : IAdmin
    {
        public bool CheckExits(string username, string email, string phone)
        {
            Admin admin = SingletonDataBridge.GetInstance().Admins.Where(a=>a.Username== username||a.Email==email||a.Phone==phone).FirstOrDefault();
            if (admin != null) return true;
            return false;
        }

        public bool CreateAdmin(AddAdmin request)
        {
            try
            {
                Admin admin = SingletonAutoMapper.GetInstance().Map<Admin>(request);
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
                ProductPrice pp = SingletonDataBridge.GetInstance().ProductPrices.Where(e => e.AdminId == id).FirstOrDefault();
                SupplyInvoice si = SingletonDataBridge.GetInstance().SupplyInvoices.Where(e => e.AdId == id).FirstOrDefault();
                if (order == null && pp == null && si == null)
                {
                    SingletonDataBridge.GetInstance().Admins.Remove(admin);
                    SingletonDataBridge.GetInstance().SaveChanges();
                    return true;
                }
                else
                {
                    admin.Status = 0;
                    SingletonDataBridge.GetInstance().Admins.Update(admin);
                    SingletonDataBridge.GetInstance().SaveChanges();
                    return true;
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

        public InfoLogin Login(string username, string password)
        {
            AdminDTO thislogin = SingletonAutoMapper.GetInstance().Map<AdminDTO>(
                SingletonDataBridge.GetInstance().Admins.Where(u => u.Username == username && u.Password == password).SingleOrDefault());
            if (thislogin != null)
            {
                var info = new InfoLogin()
                {
                    Email = thislogin.Email,
                    Name = thislogin.Name,
                    Status = thislogin.Status,
                    Phone = thislogin.Phone,
                    Username = thislogin.Username,
                    role = "ADMIN",
                };
                return info;
            }
            return null;
        }

        public bool UnLockAdmin(int id)
        {
            try
            {
                Admin admin = SingletonDataBridge.GetInstance().Admins.Find(id);
                admin.Status = 1;
                SingletonDataBridge.GetInstance().Admins.Update(admin);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAdmin(UpdateAdmin request)
        {
            try
            {
                Admin admin = SingletonDataBridge.GetInstance().Admins.Find(request.Id);
                admin.Name = request.Name;
                admin.Password = request.Password;
                admin.Phone = request.Phone;
                admin.Email = request.Email;
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
