using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Staff
{
    public int Id { get; set; }

    public int CompartmentsId { get; set; }

    public int RolesId { get; set; }

    public virtual Compartment Compartments { get; set; } = null!;

    public virtual Role Roles { get; set; } = null!;

    public virtual StaffRole? StaffRole { get; set; }
}
