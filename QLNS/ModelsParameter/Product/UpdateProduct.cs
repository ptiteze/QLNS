namespace QLNS.ModelsParameter.Product
{
    public class UpdateProduct
    {
        public int Id { get; set; } = 0;

        public int CatalogId { get; set; } = 0;

        public string Name { get; set; } = null!;
        public int Price { get; set; } = 0;

        public int Status { get; set; } = 0;

        public string Description { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? Discount { get; set; } = 0;

        public IFormFile imageFile { get; set; } = null!;

        public int? ExpiryDate { get; set; } = 0;

        public string? Unit { get; set; }
    }
}
