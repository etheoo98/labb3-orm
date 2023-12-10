using Spectre.Console;

namespace SchoolApp.Views;

public class LoginView : View
{
    public string GetUsername() => GetStringInput($"Enter your [{HighlightColor}]username:[/]");
    public string GetPassword() => GetSecretStringInput($"Enter your [{HighlightColor}]password:[/]");

    public void ShowLockoutMessage(TimeSpan timeSpan)
    {
        var timeRemaining = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";

        Console.Clear();
        AnsiConsole.Markup(
            $"You've [red]failed[/] too many login attempts and have been locked out for {timeRemaining}");
    }
}