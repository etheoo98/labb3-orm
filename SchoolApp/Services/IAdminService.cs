namespace SchoolApp.Services;

public interface IAdminService
{
    public bool CreateAdmin(string username, string password);
}