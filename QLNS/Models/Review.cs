using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ContentReview { get; set; } = null!;

    public DateOnly? Created { get; set; }

    public int? IdUser { get; set; }

    public int Score { get; set; } = 0;

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
