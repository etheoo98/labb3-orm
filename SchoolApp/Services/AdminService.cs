using Microsoft.EntityFrameworkCore;
using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public class AdminService(DatabaseContext context, DtoMapper dtoMapper) : IAdminService
{
    
    public List<StaffDto> GetStaffBasedOnRoles(List<string> roles)
    {
        var staffList = context.Staff
            .Include(s => s.Roles.Persons)
            .Include(s => s.StaffRole.StaffRoleNames)
            .Where(s => roles.Contains(s.StaffRole.StaffRoleNames.Name))
            .ToList();
        
        var staffDtoList = staffList.Select(dtoMapper.StaffDtoMapper).ToList();

        return staffDtoList;
    }
    
    public List<StudentDto> GetAllStudents(bool sortByFirstName, bool orderByDescending)
    {
        var studentList = context.Students
            .Include(s => s.Roles)
            .ThenInclude(r => r.Persons)
            .ToList();

        switch (sortByFirstName)
        {
            case true when orderByDescending:
                studentList = studentList.OrderByDescending(s => s.Roles.Persons.FirstName).ToList();
                break;
            case true when !orderByDescending:
                studentList = studentList.OrderBy(s => s.Roles.Persons.FirstName).ToList();
                break;
            case false when orderByDescending:
                studentList = studentList.OrderByDescending(s => s.Roles.Persons.LastName).ToList();
                break;
            case false when !orderByDescending:
                studentList = studentList.OrderBy(s => s.Roles.Persons.LastName).ToList();
                break;
        }

        var studentDtoList = studentList.Select(dtoMapper.StudentDtoMapper).ToList();

        return studentDtoList;
    }

    public List<GradeDto> GetRecentGrades()
    {
        var currentTime = DateTime.UtcNow;
        var currentUnixTime = ((DateTimeOffset)currentTime).ToUnixTimeMilliseconds();
        var unixTimeOneYearAgo = currentUnixTime - 31536000000;
        
        var grades = context.Grades
            .Include(g => g.Students.Roles.Persons)
            .Include(g => g.GradeValues)
            .Include(g => g.Courses)
            .Where(g => g.Date >= unixTimeOneYearAgo)
            .ToList();

        var gradeDtoList = grades.Select(dtoMapper.GradeDtoMapper).ToList();

        return gradeDtoList;
    }

    public List<CourseDto> GetCourseStatistics()
    {
        var coursesList = context.Courses
            .Include(c => c.Grades)
            .ThenInclude(g => g.GradeValues)
            .Where(c => c.Grades.Count != 0)
            .ToList();
        
        var gradeValuesList = context.GradeValues.ToList();

        var courseDtoList = coursesList
            .Select(course => dtoMapper.CourseDtoMapper(course, gradeValuesList)).ToList();

        return courseDtoList;
    }

    public List<YearGroup> GetYearGroups() => context.YearGroups.ToList();

    public List<StudentDto> GetStudentsFromYearGroup(int yearGroup)
    {
        var students = context.Students
            .Where(s => s.YearGroups.Year == yearGroup)
            .Include(s => s.Roles)
            .ThenInclude(r => r.Persons)
            .ToList();
        
        var studentDtoList = students.Select(dtoMapper.StudentDtoMapper).ToList();

        return studentDtoList;
    }
    
    public bool CreateAdmin(string username, string password)
    {
        if (context.Administrators.Any(a => a.Username == username))
        {
            return false;
        }
        
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);
        var admin = new Administrator
        {
            Username = username,
            Password = passwordHash
        };

        context.Administrators.Add(admin);
        context.SaveChanges();
        
        admin.Password = string.Empty;
        
        return true;
    }
}