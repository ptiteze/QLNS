using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class Producer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Numphone { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<SupplyInvoice> SupplyInvoices { get; set; } = new List<SupplyInvoice>();
}
