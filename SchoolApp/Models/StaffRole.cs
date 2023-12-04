using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class StaffRole
{
    public int Id { get; set; }

    public int StaffId { get; set; }

    public int StaffRoleNamesId { get; set; }

    public virtual Administrator? Administrator { get; set; }

    public virtual Principal? Principal { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual StaffRoleName StaffRoleNames { get; set; } = null!;

    public virtual Teacher? Teacher { get; set; }
}
