using QLNS.DTO;
using System.Transactions;

namespace QLNS.ViewModels.Order
{
    public class CheckoutViewModel
    {
        public List<ShowDetail> Show {  get; set; }
        public UserDTO User { get; set; }
    }
}
