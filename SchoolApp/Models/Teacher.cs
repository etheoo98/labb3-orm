using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public int StaffRolesId { get; set; }

    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual StaffRole StaffRoles { get; set; } = null!;
}
