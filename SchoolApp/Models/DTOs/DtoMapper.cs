namespace SchoolApp.Models.DTOs;

public class DtoMapper
{
    public StaffDto StaffDtoMapper(Staff staff) =>
        new(staff.Roles.Persons.FirstName, staff.Roles.Persons.LastName, staff.StaffRole.StaffRoleNames.Name);
    
    public GradeDto GradeDtoMapper(Grade grade)
    {
        var date = DateTimeOffset.FromUnixTimeMilliseconds(grade.Date).DateTime;
            
        return new GradeDto(
            grade.Students.Roles.Persons.Ssn,
            grade.Students.Roles.Persons.LastName,
            grade.Courses.Name,
            grade.GradeValues.Letter,
            date);
    }

    public StudentDto StudentDtoMapper(Student student) => 
        new(student.Id,
            student.Roles.Persons.Ssn,
            student.Roles.Persons.FirstName,
            student.Roles.Persons.LastName,
            student.YearGroups.Year,
            student.Registrations.Count);

    public CourseDto CourseDtoMapper(Course course, List<GradeValue> gradeValues)
    {
        var highestGradeValue = course.Grades.Max(g => g.GradeValues.Value);
        var averageGradeValue = (int)Math.Round(course.Grades.Select(g => g.GradeValues.Value).Average());
        var lowestGradeValue = course.Grades.Min(g => g.GradeValues.Value);
            
        var highestGradeLetter = gradeValues.FirstOrDefault(g => g.Value == highestGradeValue)!.Letter;
        var averageGradeLetter = gradeValues.FirstOrDefault(g => g.Value == averageGradeValue)!.Letter;
        var lowestGradeLetter = gradeValues.FirstOrDefault(g => g.Value == lowestGradeValue)!.Letter;
            
        return new CourseDto(course.Name, highestGradeLetter, averageGradeLetter, lowestGradeLetter);
    }

    public CompartmentDto CompartmentDtoMapper(Compartment compartment) =>
        new(compartment.Name, compartment.Salaries.Amount, compartment.Staff.Count);

    public ActiveCourseDto ActiveCourseMapper(Course course)
    {
        var teacherLastNames = course.CourseTeachers
            .Select(ct => ct.Teachers.StaffRoles.Staff.Roles.Persons.LastName).ToList();
        var teachersString = string.Join(", ", teacherLastNames);

        return new ActiveCourseDto(course.Name, teachersString, course.Registrations.Count);
    }

    public TeacherDto TeacherDtoMapper(Teacher teacher) => new(teacher.Id, teacher.StaffRoles.Staff.Roles.Persons.Ssn,
        teacher.StaffRoles.Staff.Roles.Persons.FirstName, teacher.StaffRoles.Staff.Roles.Persons.LastName);
}