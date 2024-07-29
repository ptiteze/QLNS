namespace QLNS.ModelsParameter.Product
{
    public class InputProduct
    {
        public int product_cate { get; set; } = 0;

        public string? product_name { get; set; } = null!;
        public int product_price { get; set; } = 0;

        public int product_status { get; set; } = 1;

        public string? product_desc { get; set; } = null!;

        public string? product_content { get; set; } = null!;

        public int? product_discount { get; set; } = 0;

        public IFormFile imageFile { get; set; } = null!;

        public DateOnly product_day { get; set; }

        /// <summary>
        /// day
        /// </summary>
        public int? product_expixy { get; set; } = 0;

        public string? product_unit { get; set; }
    }
}
