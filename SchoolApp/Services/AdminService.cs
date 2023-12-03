using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Services;

public class AdminService(DatabaseContext context) : IAdminService
{
    public bool CreateAdmin(string username, string password)
    {
        if (context.Administrators.Any(a => a.Username == username))
        {
            return false;
        }
        
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);
        var admin = new Administrator
        {
            Username = username,
            Password = passwordHash
        };

        context.Administrators.Add(admin);
        context.SaveChanges();
        
        admin.Password = string.Empty;
        
        return true;
    }

    public void Test()
    {
        context.Administrators.FromSqlRaw("");
    }
}