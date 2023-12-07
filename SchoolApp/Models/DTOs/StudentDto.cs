namespace SchoolApp.Models.DTOs;

public class StudentDto(string firstName, string lastName, int yearGroup, int courseCount)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public int YearGroup { get; set; } = yearGroup;
    public int CourseCount { get; set; } = courseCount;
}