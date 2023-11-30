using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
