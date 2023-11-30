using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Compartment
{
    public int Id { get; set; }

    public int? SalaryId { get; set; }

    public int? Name { get; set; }

    public virtual Salary? Salary { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
