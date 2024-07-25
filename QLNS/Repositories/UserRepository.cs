using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Singleton;

namespace QLNS.Repositories
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

        public InfoLogin Login(string username, string password)
        {
            UserDTO thislogin = SingletonAutoMapper.GetInstance().Map<UserDTO>(
                SingletonDataBridge.GetInstance().Users.Where(u => u.Username== username && u.Password== password).SingleOrDefault());
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
