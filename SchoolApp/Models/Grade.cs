using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int StudentsId { get; set; }

    public int CoursesId { get; set; }

    public int TeachersId { get; set; }

    public string StudentGrade { get; set; } = null!;

    public long Date { get; set; }

    public virtual Course Courses { get; set; } = null!;

    public virtual Student Students { get; set; } = null!;

    public virtual Teacher Teachers { get; set; } = null!;
}
