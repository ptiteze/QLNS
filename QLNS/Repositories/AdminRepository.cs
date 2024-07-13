using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class AdminRepository : IAdmin
    {
        public InfoLogin Login(string username, string password)
        {
            AdminDTO thislogin = SingletonAutoMapper.GetInstance().Map<AdminDTO>(
                SingletonDataBridge.GetInstance().Users.Where(u => u.Username == username && u.Password == password).SingleOrDefault());
            if (thislogin != null)
            {
                var info = new InfoLogin()
                {
                    Email = thislogin.Email,
                    Name = thislogin.Name,
                    Phone = thislogin.Phone,
                    Username = thislogin.Username,
                };
                return info;
            }
            return null;
        }
    }
}
