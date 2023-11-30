namespace SchoolApp;

internal static class Program
{
    public static void Main()
    {
        var context = new DatabaseContext();
        var loginPresenter = new LoginPresenter(context, new LoginServices(), new LoginView());
        
        loginPresenter.HandlePresenter();
    }
}