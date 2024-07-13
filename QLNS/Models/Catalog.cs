using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Catalog
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
