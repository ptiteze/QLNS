using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Cart
{
    public string UserName { get; set; } = null!;

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User UserNameNavigation { get; set; } = null!;
}
