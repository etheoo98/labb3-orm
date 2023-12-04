using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class GradeValue
{
    public int Id { get; set; }

    public string Letter { get; set; } = null!;

    public int Value { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
