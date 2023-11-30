namespace SchoolApp.Presenters;

public class LoginPresenter(LoginService loginService, LoginView loginView) : ILoginPresenter
{
    private Dictionary<string, Action> _loginChoices = new();
    private string? _username = "";
    private string? _password = "";
    private string? _maskedPassword = "";
        
    
    public void HandlePresenter()
    {
        const string prompt = "Please enter your admin credentials";
        var exit = false;

        while (!exit)
        {
            _loginChoices = new Dictionary<string, Action>()
            {
                {$"Username: {_username}", OnSelect_Username},
                {$"Password: {_maskedPassword}\n", OnSelect_Password},
                {"Login", OnSelect_Login},
                {"Exit", () => exit = true},
            };

            var choice = loginView.GetChoice(prompt, _loginChoices.Keys.ToList());
            
            _loginChoices[choice].Invoke();
        }
    }

    public void OnSelect_Username()
    {
        _username = loginView.GetInput("Enter your username");
    }

    public void OnSelect_Password()
    {
        _password = loginView.GetSecretInput("Enter your password");
        _maskedPassword = new string('*', _password!.Length);
    }

    public void OnSelect_Login()
    {
        if (_username == null || _password == null) return;
        
        var adminDto = loginService.AttemptLogin(_username, _password);
        
        if (adminDto != null)
        {
            throw new NotImplementedException();
        }
    }
}