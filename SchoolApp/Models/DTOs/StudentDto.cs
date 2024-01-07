namespace SchoolApp.Models.DTOs;

public class StudentDto(int studentId, string ssn, string firstName, string lastName, int yearGroup, int courseCount)
{
    public int StudentId { get; set; } = studentId;
    public string Ssn { get; set; } = ssn;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public int YearGroup { get; set; } = yearGroup;
    public int CourseCount { get; set; } = courseCount;
}