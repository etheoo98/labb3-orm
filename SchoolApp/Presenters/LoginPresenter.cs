namespace SchoolApp.Presenters;

public class LoginPresenter(ILoginService loginService, LoginView loginView, IAdminPresenter adminPresenter)
    : IPresenter
{
    private Dictionary<string, Action> _loginChoices = new();
    private string? _username;
    private string? _password;
    private string? _maskedPassword;
    private int _attempts = 3;

    public void HandlePresenter()
    {
        const string prompt = "Please enter your [blue]admin credentials[/]";
        var exit = false;

        while (!exit)
        {
            _loginChoices = new Dictionary<string, Action>
            {
                { $"Username: [blue]{_username}[/]", OnSelect_Username },
                { $"Password: [blue]{_maskedPassword}[/]\n", OnSelect_Password },
                { "Login", OnSelect_Login },
                { "Exit", () => exit = true },
            };

            RunPresenter(prompt);
        }
    }

    private void RunPresenter(string prompt)
    {
        if (_attempts != 0)
        {
            var choice = loginView.GetChoice(prompt, _loginChoices.Keys.ToList());
            _loginChoices[choice].Invoke();
        }
        else
        {
            Lockout();
        }
    }

    private void OnSelect_Username()
    {
        _username = loginView.GetUsername();
    }

    private void OnSelect_Password()
    {
        _password = loginView.GetPassword();
        _maskedPassword = new string('*', _password!.Length);
    }

    private void OnSelect_Login()
    {
        if (_username == null || _password == null || _attempts == 0) return;

        var adminDto = loginService.AttemptLogin(_username, _password);

        ResetPasswordFields();

        if (adminDto == null)
        {
            _attempts--;
            return;
        }

        ResetLoginAttempts();
        adminPresenter.HandlePresenter(adminDto);
    }

    private void ResetPasswordFields()
    {
        _password = null;
        _maskedPassword = null;
    }

    private void Lockout()
    {
        var timeSpan = TimeSpan.FromSeconds(60);

        for (var i = timeSpan; i != TimeSpan.Zero; i = i.Subtract(TimeSpan.FromSeconds(1)))
        {
            loginView.ShowLockoutMessage(i);
            Thread.Sleep(1000);
        }

        ResetLoginAttempts();
    }

    private void ResetLoginAttempts()
    {
        _attempts = 3;
    }
}