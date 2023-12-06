using Spectre.Console;

namespace SchoolApp.Views;

public abstract class View : IView
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

    public List<T> GetMultiChoice<T>(string prompt, List<T> choices) where T : notnull
    {
        var fruits = AnsiConsole.Prompt(
            new MultiSelectionPrompt<T>()
                .Title(prompt)
                .Required()
                .PageSize(10)
                .AddChoices(choices));

        return fruits;
    }
}