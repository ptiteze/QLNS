namespace QLNS.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ContentReview { get; set; } = null!;

        public DateOnly? Created { get; set; }

        public int? IdUser { get; set; }

        public int Score { get; set; } = 0;
    }
}
