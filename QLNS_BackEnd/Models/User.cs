using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class User
{
    public int Id { get; set; }

    public string Nameuser { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly Created { get; set; }

    public int? IdAccount { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Account? IdAccountNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
