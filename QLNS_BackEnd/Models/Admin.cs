using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int IdAccount { get; set; } = 0;

    public virtual Account? IdAccountNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<SupplyInvoice> SupplyInvoices { get; set; } = new List<SupplyInvoice>();
}
