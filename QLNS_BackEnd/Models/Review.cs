using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ContentReview { get; set; } = null!;

    public DateOnly? Created { get; set; }

    public string Name { get; set; } = null!;
}
