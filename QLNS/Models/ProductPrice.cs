using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class ProductPrice
{
    public int ProductId { get; set; }

    public DateOnly AppliedDate { get; set; }

    public int Price { get; set; }

    public string AdminId { get; set; } = null!;

    public virtual Admin Admin { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
