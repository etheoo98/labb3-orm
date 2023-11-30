using SchoolApp.Models.DTOs;

namespace SchoolApp.Services;

public interface ILoginServices
{
    public AdminDto AttemptLogin();
}