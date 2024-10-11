using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.ModelsParameter.User;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class UserRepository : IUser
    {
        private readonly IAccount Iacc;

        public UserRepository(IAccount accountRepository)
        {
            Iacc = accountRepository;
        }
        public bool CreateUser(AddUser request)
        {
            try
            {
                List<User> users = SingletonDataBridge.GetInstance().Users.ToList();
                foreach (User u in users)
                {
                    if (u.Email == request.Email || u.Phone == request.Phone)
                        return false;
                }
                RequestLogin requestLogin = new RequestLogin()
                {
                    username = request.Username,
                    password = request.Password
                };
                int accId = Iacc.CreateLogin(requestLogin, 2);
                if(accId == 0) return false;
                User user = new User()
                {
                    Nameuser = request.Nameuser,
                    Email = request.Email,
                    Phone = request.Phone,
                    Created = request.Created,
                    Address = request.Address,
                    IdAccount = accId
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

        public UserDTO GetUserById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<UserDTO>(
                SingletonDataBridge.GetInstance().Users.Where(u => u.Id == id).FirstOrDefault());
        }

        public List<UserDTO> GetUsers()
        {
            return SingletonAutoMapper.GetInstance().Map<List<UserDTO>>(
                SingletonDataBridge.GetInstance().Users.ToList());
        }

        public bool UpdateUser(UpdateUser request)
        {
            try
            {
                List<User> users = SingletonDataBridge.GetInstance().Users.ToList();
                User user = users.Where(u => u.Id == request.Id).FirstOrDefault();
                users.Remove(user);
                foreach (User u in users)
                {
                    if (u.Email == request.Email || u.Phone == request.Phone)
                        return false;
                }
                user.Nameuser = request.Nameuser;
                user.Email = request.Email;
                user.Phone = request.Phone;
                user.Created = request.Created;
                user.Address = request.Address; 
                if (Iacc.ChangePassword(request.Password, request.IdAccount)) return false;
                Console.WriteLine(request.Id + '-' + request.Password + '-' + request.Phone + '-' + request.Email + '-' + request.Created.ToString() + '-' + request.Nameuser);
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
