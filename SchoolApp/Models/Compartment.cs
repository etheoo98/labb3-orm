using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Compartment
{
    public int Id { get; set; }

    public int SalariesId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Salary Salaries { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
