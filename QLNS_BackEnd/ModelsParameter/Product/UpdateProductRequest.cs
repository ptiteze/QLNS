namespace QLNS_BackEnd.ModelsParameter.Product
{
	public class UpdateProductRequest
	{
		public int? Id { get; set; }

		public int CatalogId { get; set; }

		public string Name { get; set; } = null!;
		public int Price { get; set; } = 0;

		public int Status { get; set; }

		public string Description { get; set; } = null!;

		public string Content { get; set; } = null!;

		public int Discount { get; set; } = 0;

		public string ImageLink { get; set; } = null!;

		public int? ExpiryDate { get; set; }

		public string? Unit { get; set; }
	}
}
