namespace QLNS.ModelsParameter.Producer
{
	public class CreateProducerRequest
	{
		public string Name { get; set; } = null!;

		public string Address { get; set; } = null!;

		public string Numphone { get; set; } = null!;

		public string? Email { get; set; }
	}
}
