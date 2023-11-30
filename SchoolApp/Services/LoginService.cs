using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public class LoginService(DatabaseContext context) : ILoginService
{
    public AdminDto? AttemptLogin(string username, string password)
    {
        var admin = context.Administrators.FirstOrDefault(a => a.Username == username && a.Password == password);

        return admin == null ? null : new AdminDto(admin.Username);
    }
}