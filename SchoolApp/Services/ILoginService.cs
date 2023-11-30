using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public interface ILoginService
{
    public AdminDto? AttemptLogin(string username, string password);
}