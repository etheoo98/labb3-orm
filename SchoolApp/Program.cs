namespace SchoolApp;

internal static class Program
{
    public static void Main()
    {
        var context = new DatabaseContext();
        var loginPresenter = new LoginPresenter(context, new LoginService(context), new LoginView());
        
        loginPresenter.HandlePresenter();
    }
}