using Azure.Core;
using Microsoft.EntityFrameworkCore;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.Singleton;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace QLNS_BackEnd.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly IConfiguration _configuration;
        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool ChangePassword(string password, int id)
        {
            try
            {
                Account acc = SingletonDataBridge.GetInstance().Accounts.Where(a => a.Id == id).FirstOrDefault();
                if (acc == null) return false;
                acc.Password = password;
                SingletonDataBridge.GetInstance().Accounts.Update(acc);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckExitsUsermane(string username)
        {
            try
            {
                return SingletonDataBridge.GetInstance().Accounts.Any(a => a.Username==username);
            }
            catch
            {
                return false;
            }
        }

        public int CreateLogin(RequestLogin request, int role)
        {
            try
            {
                if (CheckExitsUsermane(request.username)) return 0;
                Account acc = new Account()
                {
                    Username = request.username,
                    Password = request.password,
                    IdRole = role,
                    Status = true,
                };
                SingletonDataBridge.GetInstance().Accounts.Add(acc);
                SingletonDataBridge.GetInstance().SaveChanges();
                return acc.Id;
            }catch
            {
                return 0;
            }
        }

        public async Task<bool> ForgetPass(RequestForgetPass request)
        {
            try
            {
                string gmailto = "";
                Account acc = await SingletonDataBridge.GetInstance().Accounts.FindAsync(request.Id);
                //var u = new object();
                if (request.Role == 1)
                {
                    var u = await SingletonDataBridge.GetInstance().Admins.Where(a => a.IdAccount == acc.Id).FirstOrDefaultAsync();
                    gmailto =  u.Email;
                }
                else
                {
                    var u = await SingletonDataBridge.GetInstance().Users.Where(a => a.IdAccount == acc.Id).FirstOrDefaultAsync();
                    gmailto = u.Email;
                }

                Random random = new Random();
                int randomNumber = random.Next(100000, 1000000);

                var email = _configuration.GetValue<string>("Email_Configuration:EMAIL");
                var password = _configuration.GetValue<string>("Email_Configuration:PASSWORD");
                var host = _configuration.GetValue<string>("Email_Configuration:HOST");
                var port = _configuration.GetValue<int>("Email_Configuration:PORT");

                var smtpClient = new SmtpClient(host, port);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(email, password);
                var message = new MailMessage(email!, gmailto, "Mật khẩu mới của bạn là: ", randomNumber.ToString());
                await smtpClient.SendMailAsync(message);
                acc.Password = randomNumber.ToString();
                SingletonDataBridge.GetInstance().Accounts.Update(acc);
                await SingletonDataBridge.GetInstance().SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public AccountDTO GetAccountByUsername(string username)
        {
            return SingletonAutoMapper.GetInstance().Map<AccountDTO>(SingletonDataBridge.GetInstance().Accounts.Where(a => a.Username == username).FirstOrDefault());
        }

        public List<AccountDTO> GetAccounts()
        {
            return SingletonAutoMapper.GetInstance().Map<List<AccountDTO>>(
                SingletonDataBridge.GetInstance().Accounts.ToList());
        }

        public bool LockAccount(int id)
        {
            try
            {
                Account acc = SingletonDataBridge.GetInstance().Accounts.Where(a => a.Id == id).FirstOrDefault();
                if (acc == null) return false;
                acc.Status = false;
                SingletonDataBridge.GetInstance().Accounts.Update(acc);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public InfoLogin Login(RequestLogin request)
        {
            try
            {
                InfoLogin info = null;
                Admin admin = null;
                User user = null;
                Account acc = SingletonDataBridge.GetInstance().Accounts.Where(a => a.Username == request.username && 
                a.Password == request.password).FirstOrDefault();
                if (acc == null) return null;
                if(acc.IdRole == 2)
                {
                    user = SingletonDataBridge.GetInstance().Users.Where(u => u.IdAccount == acc.Id).FirstOrDefault();
                    info = new InfoLogin()
                    {
                        IdUser = user.Id,
                        Username = acc.Username,
                        Email = user.Email,
                        Name = user.Nameuser,
                        Phone = user.Phone,
                        role = SingletonDataBridge.GetInstance().Roles.Find(acc.IdRole).Name,
                        Status = acc.Status
                    };
                }
                else
                {
                    admin = SingletonDataBridge.GetInstance().Admins.Where(u => u.IdAccount == acc.Id).FirstOrDefault();
                    info = new InfoLogin()
                    {
                        IdUser = admin.Id,
                        Username = acc.Username,
                        Email = admin.Email,
                        Name = admin.Name,
                        Phone = admin.Phone,
                        role = SingletonDataBridge.GetInstance().Roles.Find(acc.IdRole).Name,
                        Status = acc.Status
                    };
                }
                return info;
            }
            catch
            {
                return null;
            }
        }

        public bool UnlockAccount(int id)
        {
            try
            {
                Account acc = SingletonDataBridge.GetInstance().Accounts.Where(a => a.Id == id).FirstOrDefault();
                if (acc == null) return false;
                acc.Status = true;
                SingletonDataBridge.GetInstance().Accounts.Update(acc);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
