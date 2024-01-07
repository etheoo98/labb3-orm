using Microsoft.EntityFrameworkCore;
using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public class AdminService(DatabaseContext context, DtoMapper dtoMapper) : IAdminService
{
    public List<string> GetStaffRoleNames() => context.StaffRoleNames.Select(srn => srn.Name).ToList();
    public List<int> GetYearGroups() => context.YearGroups.Select(yg => yg.Year).ToList();
    public List<string> GetCompartments() => context.Compartments.Select(c => c.Name).ToList();
    public List<GradeValue> GetGradesValues() => context.GradeValues.ToList();

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
            .Include(s => s.YearGroups)
            .Include(s => s.Registrations)
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

        var courseDtoList = coursesList.Select(course => dtoMapper.CourseDtoMapper(course, gradeValuesList)).ToList();

        return courseDtoList;
    }

    public List<StudentDto> GetStudentsFromYearGroup(int yearGroup)
    {
        var students = context.Students.Where(s => s.YearGroups.Year == yearGroup)
            .Include(s => s.Roles)
            .ThenInclude(r => r.Persons)
            .Include(s => s.YearGroups)
            .ToList();

        var studentDtoList = students.Select(dtoMapper.StudentDtoMapper).ToList();

        return studentDtoList;
    }

    public bool AddStudent(Person person, int year)
    {
        var duplicateSsn = context.Persons.Any(p => p.Ssn == person.Ssn);

        if (duplicateSsn) return false;

        using var transaction = context.Database.BeginTransaction();
        try
        {
            context.Persons.Add(person);
            context.SaveChanges();

            var role = new Role { PersonsId = person.Id };
            
            context.Roles.Add(role);
            context.SaveChanges();

            var yearGroupId = context.YearGroups.Where(yg => yg.Year == year).Select(yg => yg.Id).SingleOrDefault();
            var student = new Student { RolesId = role.Id, YearGroupsId = yearGroupId };
            
            context.Students.Add(student);
            context.SaveChanges();
            transaction.Commit();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<CompartmentDto> GetCompartmentOverview()
    {
        var compartments = context.Compartments.Include(c => c.Staff)
            .Include(c => c.Salaries).ToList();

        return compartments.Select(dtoMapper.CompartmentDtoMapper).ToList();
    }

    public List<ActiveCourseDto> GetActiveCourses()
    {
        var courses = context.Courses
            .Include(c => c.CourseTeachers)
            .ThenInclude(ct => ct.Teachers.StaffRoles.Staff.Roles.Persons)
            .Include(c => c.Registrations)
            .Where(c => c.CourseTeachers.Count != 0 && c.Registrations.Count != 0)
            .ToList();

        return courses.Select(dtoMapper.ActiveCourseMapper).ToList();
    }

    public List<Course> GetStudentsCourses(string ssn) =>
        context.Courses.Where(c => c.Registrations.Any(r => r.Students.Roles.Persons.Ssn == ssn)).ToList();

    public List<TeacherDto> GetAllTeachers()
    {
        var teachers = context.Teachers.Include(t => t.StaffRoles.Staff.Roles.Persons).ToList();

        return teachers.Select(dtoMapper.TeacherDtoMapper).ToList();
    }

    public bool GradeStudent(StudentDto student, TeacherDto teacher, Course course, GradeValue gradeValue)
    {
        using var transaction = context.Database.BeginTransaction();
        
        try
        {
            ApplyGrade(student, teacher, course, gradeValue);
            RemoveRegistration(student, course);
            
            context.SaveChanges();
            transaction.Commit();
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            return false;
        }
    }
    
    public bool AddAdministrator(Person person, string staffRoleName, string compartmentName, string username, string password)
    {
        using var transaction = context.Database.BeginTransaction();
        
        try
        {
            var staffRoleId = AddStaff(person, staffRoleName, compartmentName);
            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);
            
            context.Administrators.Add(new Administrator { StaffRolesId = staffRoleId, Username = username, Password = passwordHash });
            
            context.SaveChanges();
            transaction.Commit();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool AddPrincipal(Person person, string staffRoleName, string compartmentName)
    {
        using var transaction = context.Database.BeginTransaction();
        
        try
        {
            var staffRoleId = AddStaff(person, staffRoleName, compartmentName);
            
            context.Principals.Add(new Principal { StaffRolesId = staffRoleId });
            context.SaveChanges();
            transaction.Commit();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return false;
        }
    }

    public bool AddTeacher(Person person, string staffRoleName, string compartmentName)
    {
        using var transaction = context.Database.BeginTransaction();
        
        try
        {
            var staffRoleId = AddStaff(person, staffRoleName, compartmentName);
            
            context.Teachers.Add(new Teacher { StaffRolesId = staffRoleId });
            context.SaveChanges();
            transaction.Commit();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return false;
        }
    }

    private void ApplyGrade(StudentDto student, TeacherDto teacher, Course course, GradeValue gradeValue)
    {
        var currentTime = DateTimeOffset.UtcNow;
        var unixTimeMilliseconds = currentTime.ToUnixTimeMilliseconds();
        
        context.Grades.Add(new Grade
        {
            StudentsId = student.StudentId,
            CoursesId = course.Id,
            TeachersId = teacher.TeacherId,
            GradeValuesId = gradeValue.Id,
            Date = unixTimeMilliseconds,
        });
    }
    
    private void RemoveRegistration(StudentDto student, Course course)
    {
        var registration = context.Registrations.First(r => r.StudentsId == student.StudentId && r.CoursesId == course.Id);

        context.Registrations.Remove(registration);
    }

    private int AddStaff(Person person, string staffRoleName, string compartmentName)
    {
        var compartmentId = GetCompartmentId(compartmentName);
        var personId = AddPerson(person);
        var roleId = AddRole(personId);
        var staffId = AddStaff(compartmentId, roleId);
        var staffRoleNameId = AddStaffRoleName(staffRoleName);
        var staffRoleId = AddStaffRole(staffRoleNameId, staffId);

        return staffRoleId;
    }

    private int GetCompartmentId(string compartment) => 
        context.Compartments.Where(c => c.Name == compartment).Select(c => c.Id).SingleOrDefault();

    private int AddPerson(Person person)
    {
        context.Persons.Add(person);
        context.SaveChanges();

        return person.Id;
    }

    private int AddRole(int personId)
    {
        var role = new Role { PersonsId = personId };
        
        context.Roles.Add(role);
        context.SaveChanges();

        return role.Id;
    }

    private int AddStaff(int compartmentId, int roleId)
    {
        var staff = new Staff { CompartmentsId = compartmentId, RolesId = roleId };
        
        context.Staff.Add(staff);
        context.SaveChanges();

        return staff.Id;
    }

    private int AddStaffRoleName(string staffRoleName)
    {
        var staffRoleNameId = context.StaffRoleNames
            .Where(srn => srn.Name == staffRoleName)
            .Select(srn => srn.Id)
            .SingleOrDefault();

        return staffRoleNameId;
    }

    private int AddStaffRole(int staffRoleNameId, int staffId)
    {
        var staffRole = new StaffRole { StaffRoleNamesId = staffRoleNameId, StaffId = staffId };
        
        context.StaffRoles.Add(staffRole);
        context.SaveChanges();

        return staffRole.Id;
    }
}