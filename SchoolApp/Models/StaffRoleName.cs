using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class StaffRoleName
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StaffRole> StaffRoles { get; set; } = new List<StaffRole>();
}
