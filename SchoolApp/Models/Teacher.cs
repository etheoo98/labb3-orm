namespace SchoolApp.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Role RoleNameNavigation { get; set; } = null!;
}
