using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Admin;

namespace QLNS_BackEnd.Interfaces
{
    public interface IAccount
    {
        InfoLogin Login(RequestLogin request);
        AccountDTO GetAccountByUsername(string username);
        bool CheckExitsUsermane(string username);
        int CreateLogin(RequestLogin request, int role);
        bool ChangePassword(string password, int id);
        bool LockAccount(int id);
        bool UnlockAccount(int id);
        List<AccountDTO> GetAccounts();
        Task<bool> ForgetPass(RequestForgetPass request);
    }
}
