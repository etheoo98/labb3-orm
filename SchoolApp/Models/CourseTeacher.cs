using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class CourseTeacher
{
    public int Id { get; set; }

    public int CoursesId { get; set; }

    public int TeachersId { get; set; }

    public virtual Course Courses { get; set; } = null!;

    public virtual Teacher Teachers { get; set; } = null!;
}
