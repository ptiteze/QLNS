using QLNS.DTO;

namespace QLNS.ViewModels.Order
{
    public class ShowOrderViewModel
    {
        public Dictionary<OrderDTO, int> orders { get; set; }
        public List<TransactionDTO> transactions { get; set; }
    }
}
