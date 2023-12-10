using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Models.DTOs;

namespace SchoolApp;

internal static class Program
{
    public static void Main()
    { 
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddScoped<DatabaseContext>();
        serviceCollection.AddScoped<ILoginService, LoginService>();
        serviceCollection.AddScoped<LoginView>();
        serviceCollection.AddScoped<LoginPresenter>();
        serviceCollection.AddScoped<IAdminPresenter, AdminPresenter>();
        serviceCollection.AddScoped<AdminView>();
        serviceCollection.AddScoped<IAdminService, AdminService>();
        serviceCollection.AddScoped<AddStudentView>();
        serviceCollection.AddTransient<AddStudentPresenter>();
        serviceCollection.AddScoped<AddStaffView>();
        serviceCollection.AddTransient<AddStaffPresenter>();
        serviceCollection.AddScoped<DtoMapper>();
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var loginPresenter = serviceProvider.GetRequiredService<LoginPresenter>();
        
        loginPresenter.HandlePresenter();
    }
}