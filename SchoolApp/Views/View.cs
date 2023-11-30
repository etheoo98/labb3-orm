using Spectre.Console;

namespace SchoolApp.Views;

public abstract class View
{
    public T GetChoice<T>(string prompt, List<T> choices) where T : notnull => AnsiConsole.Prompt(
        new SelectionPrompt<T>()
            .Title(prompt)
            .PageSize(10)
            .AddChoices(choices));

    public string? GetInput(string prompt) => AnsiConsole.Prompt(
        new TextPrompt<string>(prompt)
            .PromptStyle("red"));
    
    public string? GetSecretInput(string prompt) => AnsiConsole.Prompt(
        new TextPrompt<string>(prompt)
            .PromptStyle("red")
            .Secret());
}