using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public int YearGroupsId { get; set; }

    public int RolesId { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual Role Roles { get; set; } = null!;

    public virtual YearGroup YearGroups { get; set; } = null!;
}
