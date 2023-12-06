namespace SchoolApp.Models.DTOs;

public class StudentDto(string firstName, string lastName)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}