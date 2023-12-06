using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public interface IAdminService
{
    public List<StaffDto> GetStaffBasedOnRoles(List<string> roles);
    public List<YearGroup> GetYearGroups();
    public List<StudentDto> GetStudentsFromYearGroup(int yearGroup);
    public List<StudentDto> GetAllStudents(bool sortByFirstName, bool orderByDescending);
    public List<GradeDto> GetRecentGrades();
    public List<CourseDto> GetCourseStatistics();
    public bool CreateAdmin(string username, string password);
}