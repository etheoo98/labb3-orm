﻿using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Registration
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Student? Student { get; set; }
}