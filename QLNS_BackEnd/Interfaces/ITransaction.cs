using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Interfaces
{
    public interface ITransaction
    {
        bool CreateTransaction(CreateTransactionRequest request);
        TransactionDTO GetTransactionByOrderId(int id);
        TransactionDTO GetTransactionByUserName(string userName);
    }
}
