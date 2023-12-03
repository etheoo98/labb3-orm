using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class YearGroup
{
    public int Id { get; set; }

    public int Year { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
