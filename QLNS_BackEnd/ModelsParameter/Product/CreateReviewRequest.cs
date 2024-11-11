namespace QLNS_BackEnd.ModelsParameter.Product
{
    public class CreateReviewRequest
    {
        public int ProductId { get; set; }

        public string ContentReview { get; set; } = null!;

        public DateOnly? Created { get; set; }

        public int? IdUser { get; set; }

        public int Score { get; set; } = 0;
    }
}
