namespace SchoolApp.Models.DTOs;

public class GradeDto(string firstName, string lastName, string course, string grade, DateTime date)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Course { get; set; } = course;
    public string Grade { get; set; } = grade;
    public DateTime Date { get; set; } = date;
}