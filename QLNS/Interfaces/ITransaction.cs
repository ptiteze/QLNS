using QLNS.DTO;
using QLNS.ModelsParameter.Order;

namespace QLNS.Interfaces
{
    public interface ITransaction
    {
        Task<bool> CreateTransaction(CreateTransactionRequest request);
        Task<TransactionDTO> GetTransactionByOrderId(int id);
        Task<TransactionDTO> GetTransactionByUserName(string userName);
        Task<List<TransactionDTO>> GetTransactions();
    }
}
