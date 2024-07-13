using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface ICart
    {
        List<CartDTO> GetCartsByUsername(string username);
    }
}
