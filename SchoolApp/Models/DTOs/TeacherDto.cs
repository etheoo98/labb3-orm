namespace SchoolApp.Models.DTOs;

public class TeacherDto(int teacherId, string ssn, string firstName, string lastName)
{
    public int TeacherId { get; set; } = teacherId;
    public string Ssn { get; set; } = ssn;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}