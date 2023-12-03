using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Administrator
{
    public int Id { get; set; }

    public int StaffRolesId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual StaffRole StaffRoles { get; set; } = null!;
}
