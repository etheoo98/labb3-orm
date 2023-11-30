namespace SchoolApp.Models;

public partial class Staff
{
    public int Id { get; set; }

    public int? PersonalInfoId { get; set; }

    public int? CompartmentId { get; set; }

    public int? RoleId { get; set; }

    public virtual Compartment? Compartment { get; set; }

    public virtual PersonalInfo? PersonalInfo { get; set; }

    public virtual Role? Role { get; set; }
}
