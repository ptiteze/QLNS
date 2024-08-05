using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Order;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class TransactionRepository : ITransaction
    {
        public bool CreateTransaction(CreateTransactionRequest request)
        {
            try
            {
                Transaction transaction = SingletonAutoMapper.GetInstance().Map<Transaction>(request);
                SingletonDataBridge.GetInstance().Add(transaction);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public TransactionDTO GetTransactionByOrderId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<TransactionDTO>(
                SingletonDataBridge.GetInstance().Transactions.Where(t=>t.OrderId==id).FirstOrDefault());
        }

        public TransactionDTO GetTransactionByUserName(string userName)
        {
            return SingletonAutoMapper.GetInstance().Map<TransactionDTO>(
                 SingletonDataBridge.GetInstance().Transactions.Where(t => t.UserName == userName).OrderByDescending(t=>t.Created).FirstOrDefault());
        }

        public List<TransactionDTO> GetTransactions()
        {
            return SingletonAutoMapper.GetInstance().Map<List<TransactionDTO>>(
                SingletonDataBridge.GetInstance().Transactions.ToList());
        }
    }
}
