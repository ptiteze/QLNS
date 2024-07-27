using QLNS.Models;

namespace QLNS.Interfaces
{
    public interface IBoardnew
    {
        Task<List<Boardnew>> GetBoardnews();
        Task<Boardnew> GetBoardnewById(int id);
    }
}
