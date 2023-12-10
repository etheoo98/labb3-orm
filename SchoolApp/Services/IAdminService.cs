using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public interface IAdminService
{
    public List<string> GetStaffRoleNames();
    public List<StaffDto> GetStaffBasedOnRoles(List<string> roles);
    public List<int> GetYearGroups();
    public List<StudentDto> GetStudentsFromYearGroup(int yearGroup);
    public List<StudentDto> GetAllStudents(bool sortByFirstName, bool orderByDescending);
    public List<GradeDto> GetRecentGrades();
    public List<CourseDto> GetCourseStatistics();
    public bool AddStudent(Person person, int year);
    public List<string> GetCompartments();

    public bool AddAdministrator(Person person, string staffRoleName, string compartmentName, string username,
        string password);

    public bool AddPrincipal(Person person, string staffRoleName, string compartmentName);
    public bool AddTeacher(Person person, string staffRoleName, string compartmentName);
}