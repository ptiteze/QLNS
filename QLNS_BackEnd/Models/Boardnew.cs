using System;
using System.Collections.Generic;

namespace QLNS_BackEnd.Models;

public partial class Boardnew
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string ImageLink { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateOnly Created { get; set; }
}
