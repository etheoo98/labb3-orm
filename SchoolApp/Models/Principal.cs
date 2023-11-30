namespace SchoolApp.Models;

public partial class Principal
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual Role RoleNameNavigation { get; set; } = null!;
}
