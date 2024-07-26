using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly Sentdate { get; set; }

    public string Address { get; set; } = null!;

    public DateOnly? Receiveddate { get; set; }

    /// <summary>
    /// 0 : chưa hoàn thành, 1 hoàn thành
    /// </summary>
    public int? Status { get; set; }

    public string UserName { get; set; } = null!;

    public int? AdminId { get; set; }

    public virtual ICollection<Ordered> Ordereds { get; set; } = new List<Ordered>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User UserName1 { get; set; } = null!;

    public virtual Admin UserNameNavigation { get; set; } = null!;
}
