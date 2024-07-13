﻿using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserMail { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Amount { get; set; } = null!;

    public string Payment { get; set; } = null!;

    public string? Status { get; set; }

    public DateOnly Created { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
