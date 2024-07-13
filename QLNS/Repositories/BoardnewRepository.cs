using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Singleton;

namespace QLNS.Repositories
{
	public class BoardnewRepository : IBoardnew
	{
        public Boardnew GetBoardnewById(int id)
        {
			return SingletonDataBridge.GetInstance().Boardnews.Find(id);
        }

        public List<Boardnew> GetBoardnews()
		{
			return SingletonDataBridge.GetInstance().Boardnews.ToList();
		}
	}
}
