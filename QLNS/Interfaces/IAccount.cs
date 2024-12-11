using QLNS.DTO;
using QLNS.ModelsParameter.Admin;

namespace QLNS.Interfaces
{
    public interface IAccount
    {
        Task<InfoLogin> Login(RequestLogin request);
        Task<bool> Lock(int id);
        Task<bool> UnLock(int id);
        Task<AccountDTO> GetAccountByUsername(string username);
        Task<List<AccountDTO>> GetAccounts();
        Task<bool> ForgetPass(RequestForgetPass request);
    }
}
