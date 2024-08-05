using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class UserRepository : IUser
    {
        public bool CreateUser(UserDTO request)
        {
            try
            {
                List<User> users = SingletonDataBridge.GetInstance().Users.ToList();
                foreach(User u in users)
                {
                    if (u.Username == request.Username || u.Email == request.Email || u.Phone == request.Phone)
                        return false;
                }
                User user = new User() {
                    Username = request.Username,
                    Nameuser = request.Nameuser,
                    Email = request.Email,
                    Password = request.Password,
                    Phone = request.Phone,
                    Status = 1,
                    Created = request.Created,
                };
                SingletonDataBridge.GetInstance().Users.Add(user);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

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
                //if(thislogin.Status == 0) { return null; }
                var info = new InfoLogin()
                {
                    Email = thislogin.Email,
                    Name = thislogin.Nameuser,
                    Status = thislogin.Status,
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

        public bool UpdateUser(UserDTO request)
        {
            try
            {
                List<User> users = SingletonDataBridge.GetInstance().Users.ToList();
                User user = users.Where(u => u.Username == request.Username).FirstOrDefault();
                users.Remove(user);
                foreach (User u in users)
                {
                    if (u.Email == request.Email || u.Phone == request.Phone)
                        return false;
                }

                user.Username = request.Username;
                user.Nameuser = request.Nameuser;
                user.Email = request.Email;
                user.Password = request.Password;
                user.Phone = request.Phone;
                user.Status = 1;
                user.Created = request.Created;
                
                Console.WriteLine(request.Username + '-' + request.Password + '-' + request.Status.ToString()
                     + '-' + request.Phone + '-' + request.Email + '-' + request.Created.ToString() + '-' + request.Nameuser);
                Console.WriteLine(user.Username);
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
