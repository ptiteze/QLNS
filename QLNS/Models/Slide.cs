using System;
using System.Collections.Generic;

namespace QLNS.Models;

public partial class Slide
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? DiscountContent { get; set; }

    public string ContentSlide { get; set; } = null!;

    public string ImageLink { get; set; } = null!;
}
