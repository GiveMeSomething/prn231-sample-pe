using System;
using System.Collections.Generic;

namespace DemoPE.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
