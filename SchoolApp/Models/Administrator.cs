using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Administrator
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual Role? RoleNameNavigation { get; set; }
}
