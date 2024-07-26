using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class UserRepository : IUser
    {
        public List<UserDTO> GetUsers()
        {
            return SingletonAutoMapper.GetInstance().Map<List<UserDTO>>(
                SingletonDataBridge.GetInstance().Users.ToList());
        }

        public bool LockUser(string username)
        {
            try
            {
                User user = SingletonDataBridge.GetInstance().Users.Find(username);
                user.Status = 0;
                SingletonDataBridge.GetInstance().Users.Update(user);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public InfoLogin Login(RequestLogin request)
        {
            UserDTO thislogin = SingletonAutoMapper.GetInstance().Map<UserDTO>(
                SingletonDataBridge.GetInstance().Users.Where(u => u.Username== request.username && u.Password== request.password).SingleOrDefault());
            if(thislogin != null)
            {
                var info = new InfoLogin()
                {
                    Email = thislogin.Email,
                    Name = thislogin.Nameuser,
                    Phone = thislogin.Phone,
                    Username = thislogin.Username,
                    role = "KHACH HANG",
                };
                return info;
            }
            return null;
        }

        public bool UnLockUser(string username)
        {
            try
            {
                User user = SingletonDataBridge.GetInstance().Users.Find(username);
                user.Status = 1;
                SingletonDataBridge.GetInstance().Users.Update(user);
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
