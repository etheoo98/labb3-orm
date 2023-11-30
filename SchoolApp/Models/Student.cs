namespace SchoolApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public int? PersonalInfoId { get; set; }

    public virtual PersonalInfo? PersonalInfo { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
