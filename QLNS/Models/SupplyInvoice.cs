using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class SupplyInvoice
{
    public int Id { get; set; }

    public DateOnly SupplyTime { get; set; }

    public int AdId { get; set; } = 0;

    public int ProducerId { get; set; }

    public virtual Admin Ad { get; set; } = null!;

    public virtual ICollection<ImportDetail> ImportDetails { get; set; } = new List<ImportDetail>();

    public virtual Producer Producer { get; set; } = null!;
}
