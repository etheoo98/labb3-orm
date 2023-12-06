namespace SchoolApp.Models.DTOs;

public class StaffDto(string firstName, string lastName, string role)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Role { get; set; } = role;
}