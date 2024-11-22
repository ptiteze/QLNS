namespace QLNS.Interfaces
{
	public interface IRecommendation
	{
		Task<string> GetUseds(List<int> listProduct);
		Task<bool> BuildDataset();
	}
}
