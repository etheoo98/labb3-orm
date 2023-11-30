using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Principal
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual Role? RoleNameNavigation { get; set; }
}
