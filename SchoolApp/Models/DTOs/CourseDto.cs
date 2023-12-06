namespace SchoolApp.Models.DTOs;

public class CourseDto(string course, string highestGrade, string averageGrade, string lowestGrade)
{
    public string Course { get; set; } = course;
    public string HighestGrade { get; set; } = highestGrade;
    public string AverageGrade { get; set; } = averageGrade;
    public string LowestGrade { get; set; } = lowestGrade;
}