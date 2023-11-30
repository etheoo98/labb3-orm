namespace SchoolApp.Models;

public partial class Administrator
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role RoleNameNavigation { get; set; } = null!;
}
