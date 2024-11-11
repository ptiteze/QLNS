using QLNS_BackEnd.Models;

namespace QLNS_BackEnd.Interfaces
{
    public interface IBoardnew
    {
        Task<List<Boardnew>> GetBoardnews();
        Boardnew GetBoardnewById(int id);
    }
}
