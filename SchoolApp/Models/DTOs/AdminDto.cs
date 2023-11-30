namespace SchoolApp.Models.DTOs;

public class AdminDto(string username)
{
    public string Username { get; set; } = username;
}