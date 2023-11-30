using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class CourseTeacher
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    public int? TeacherId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
