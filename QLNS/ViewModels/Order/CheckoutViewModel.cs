using QLNS.DTO;
using System.Transactions;

namespace QLNS.ViewModels.Order
{
    public class CheckoutViewModel
    {
        public List<ShowDetail> Show {  get; set; }
        public TransactionDTO Transaction { get; set; } = null;
    }
}
