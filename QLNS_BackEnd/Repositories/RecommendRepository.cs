using QLNS_BackEnd.Models;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Singleton;
using Microsoft.IdentityModel.Tokens;

namespace QLNS_BackEnd.Repositories
{
	public class RecommendRepository : IRecommend
	{
		public string GetUseds(List<int> listProduct)
		{
			string result = "";
			if (listProduct.IsNullOrEmpty()) return result;
			List<Used> used = SingletonDataBridge.GetInstance().Useds.ToList();
			List<UsedProduct>  usedProducts = SingletonDataBridge.GetInstance().UsedProducts.ToList();
			Dictionary<int, int> Dict_used_count = new Dictionary<int, int>();
			foreach(int i in listProduct)
			{
				List<UsedProduct> templist = usedProducts.Where(up=>up.ProductId == i).ToList();
				foreach(UsedProduct up in templist)
				{
					if (Dict_used_count.ContainsKey(up.UsedId))
					{
						Dict_used_count[up.UsedId]++;
					}
					else
					{
						Dict_used_count[up.UsedId] = 1;
					}
				}
			}
			List<int> top2 = Dict_used_count.OrderByDescending(pair => pair.Value).Take(2)
											.Select(pair=>pair.Key).ToList();
			if (top2.IsNullOrEmpty()) return result;
			if(top2.Count() == 1) 
			{
				string s = used.Where(u => u.Id == top2[0]).FirstOrDefault().Name;
				if(s.IsNullOrEmpty()) return result;
				return s;
			}
			else
			{
				string s = used.Where(u => u.Id == top2[0]).FirstOrDefault().Name;
				s += " và " + used.Where(u => u.Id == top2[1]).FirstOrDefault().Name;
				return s;
			}
		}
	}
}
