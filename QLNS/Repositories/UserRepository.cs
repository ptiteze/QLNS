using QLNS.DTO;
using QLNS.Interfaces;
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
                };
                return info;
            }
            return null;
        }
    }
}
