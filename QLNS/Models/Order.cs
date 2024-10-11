using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly Sentdate { get; set; }

    public string Address { get; set; } = null!;

    public DateOnly? Receiveddate { get; set; }

    public string? UserName { get; set; }

    public string? UserMail { get; set; }

    public string? UserPhone { get; set; }

    public string? Payment { get; set; }

    public string? Message { get; set; }

    public int? Amount { get; set; }

    /// <summary>
    /// 0 : chưa hoàn thành, 1 hoàn thành
    /// </summary>
    public int? Status { get; set; }

    public int AdminId { get; set; }

    public int UserId { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual ICollection<Ordered> Ordereds { get; set; } = new List<Ordered>();

    public virtual OrderStatus? StatusNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
