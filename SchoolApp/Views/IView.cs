namespace SchoolApp.Views;

public interface IView
{
    public T GetChoice<T>(string prompt, List<T> choices) where T : notnull;
    public string? GetInput(string prompt);
    public string? GetSecretInput(string prompt);
}