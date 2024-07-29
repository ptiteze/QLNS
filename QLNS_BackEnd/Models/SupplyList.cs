﻿namespace QLNS_BackEnd.Models
{
    public partial class SupplyList
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}