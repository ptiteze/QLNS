﻿using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class ImportDetail
{
    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int ImportPrice { get; set; }

    public int QuantityImport { get; set; }

    public int? Stock { get; set; }

    public bool? Status { get; set; }

    public byte[]? S { get; set; }

    public virtual SupplyInvoice Invoice { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
