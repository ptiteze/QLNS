using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class AdminRepository : IAdmin
    {
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
                    Phone = thislogin.Phone,
                    Username = thislogin.Username,
                    role = "ADMIN",
                };
                return info;
            }
            return null;
        }
    }
}
