using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
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
