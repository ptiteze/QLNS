using QLNS.Models;

namespace QLNS.Interfaces
{
    public interface IBoardnew
    {
        List<Boardnew> GetBoardnews();
        Boardnew GetBoardnewById(int id);
    }
}
