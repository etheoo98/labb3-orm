namespace SchoolApp;

internal static class Program
{
    public static void Main()
    {
        var context = new DatabaseContext();
        var loginPresenter = new LoginPresenter(new LoginService(context), new LoginView());
        
        loginPresenter.HandlePresenter();
    }
}