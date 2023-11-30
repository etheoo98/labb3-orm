using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public class LoginService(DatabaseContext context) : ILoginService
{
    public AdminDto? AttemptLogin(string username, string password)
    {
        var admin = context.Administrators.FirstOrDefault(a => a.Username == username);

        return admin != null && BCrypt.Net.BCrypt.EnhancedVerify(password, admin.Password) 
            ? new AdminDto(admin.Username)
            : null;
    }
}