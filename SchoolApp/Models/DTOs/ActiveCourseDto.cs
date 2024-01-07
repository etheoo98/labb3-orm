namespace SchoolApp.Models.DTOs;

public class ActiveCourseDto(string name, string teachers, int studentCount)
{
    public string Name { get; set; } = name;
    public string Teachers { get; set; } = teachers;
    public int StudentCount { get; set; } = studentCount;
}