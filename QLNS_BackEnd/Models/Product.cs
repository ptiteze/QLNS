using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class Product
{
    public int Id { get; set; }

    public int CatalogId { get; set; }

    public string Name { get; set; } = null!;
    public int? Quantity { get; set; }  = null;

    public int Price { get; set; } = 0;

    public int Status { get; set; }

    public string Description { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int Discount { get; set; } = 0;

    public string ImageLink { get; set; } = null!;

    public DateOnly Created { get; set; }

    /// <summary>
    /// day
    /// </summary>
    public int? ExpiryDate { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Catalog Catalog { get; set; } = null!;

    public virtual ICollection<ImportDetail> ImportDetails { get; set; } = new List<ImportDetail>();

    public virtual ICollection<Ordered> Ordereds { get; set; } = new List<Ordered>();

    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    public virtual ICollection<SupplyList> SupplyLists { get; set; } = new List<SupplyList>();
}
