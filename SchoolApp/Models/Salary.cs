using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Salary
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public virtual ICollection<Compartment> Compartments { get; set; } = new List<Compartment>();
}
