using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int? RegistrationId { get; set; }

    public int? CourseId { get; set; }

    public int? TeacherId { get; set; }

    public string? Grade1 { get; set; }

    public DateTime? Date { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Registration? Registration { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
