namespace SchoolApp.Models.DTOs;

public class DtoMapper
{
    public StaffDto StaffDtoMapper(Staff staff) =>
        new(staff.Roles.Persons.FirstName, staff.Roles.Persons.LastName, staff.StaffRole.StaffRoleNames.Name);
    
    public GradeDto GradeDtoMapper(Grade grade)
    {
        var date = DateTimeOffset.FromUnixTimeMilliseconds(grade.Date).DateTime;
            
        return new GradeDto(
            grade.Students.Roles.Persons.FirstName,
            grade.Students.Roles.Persons.LastName,
            grade.Courses.Name,
            grade.GradeValues.Letter,
            date);
    }

    public StudentDto StudentDtoMapper(Student student) => 
        new(student.Roles.Persons.FirstName, student.Roles.Persons.LastName);

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
}