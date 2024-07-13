using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Admin
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();

    public virtual ICollection<SupplyInvoice> SupplyInvoices { get; set; } = new List<SupplyInvoice>();
}
