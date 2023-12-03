using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Registration
{
    public int Id { get; set; }

    public int StudentsId { get; set; }

    public int CoursesId { get; set; }

    public virtual Course Courses { get; set; } = null!;

    public virtual Student Students { get; set; } = null!;
}
