using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class ProductPrice
{
    public int ProductId { get; set; }

    public DateOnly AppliedDate { get; set; }

    public int Price { get; set; }

    public int AdminId { get; set; } = 0;

    public virtual Admin Admin { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
