namespace SchoolApp.Models.DTOs;

public class GradeDto(long ssn, string lastName, string course, string grade, DateTime date)
{
    public long Ssn { get; set; } = ssn;
    public string LastName { get; set; } = lastName;
    public string Course { get; set; } = course;
    public string Grade { get; set; } = grade;
    public DateTime Date { get; set; } = date;
}