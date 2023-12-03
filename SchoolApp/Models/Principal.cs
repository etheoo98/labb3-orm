using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Principal
{
    public int Id { get; set; }

    public int StaffRolesId { get; set; }

    public virtual StaffRole StaffRoles { get; set; } = null!;
}
