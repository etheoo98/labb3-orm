namespace SchoolApp.Services;

public class AdminService(DatabaseContext context)
{
    public bool CreateAdmin(string username, string password)
    {
        if (context.Administrators.Any(a => a.Username == username))
        {
            return false;
        }
        
        if (!context.Roles.Any(r => r.Name == "Administrator"))
        {
            context.Roles.Add(new Role { Name = "Administrator" });
            context.SaveChanges();
        }
        
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);
        var admin = new Administrator
        {
            RoleName = "Administrator",
            Username = username,
            Password = passwordHash
        };

        context.Administrators.Add(admin);
        context.SaveChanges();
        
        return true;
    }
}