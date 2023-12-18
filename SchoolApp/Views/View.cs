using Spectre.Console;

namespace SchoolApp.Views;

public abstract class View
{
    protected const string HighlightColor = "SpringGreen3";
    protected const string PromptStyleColor = "blue";

    public T GetChoice<T>(string prompt, List<T> choices) where T : notnull
    {
        Console.Clear();
        
        return AnsiConsole.Prompt(new SelectionPrompt<T>().Title(prompt)
            .HighlightStyle(Color.SpringGreen3)
            .PageSize(10)
            .AddChoices(choices));
    }


    protected string GetStringInput(string prompt)
    {
        Console.Clear();
        
        return AnsiConsole.Prompt(new TextPrompt<string>(prompt).PromptStyle($"{PromptStyleColor}").AllowEmpty());
    }


    protected string GetSecretStringInput(string prompt)
    {
        Console.Clear();
        
        return AnsiConsole.Prompt(new TextPrompt<string>(prompt).PromptStyle($"{PromptStyleColor}").Secret().AllowEmpty());
    }
        

    protected void ShowMessage(string message)
    {
        Console.Clear();
        AnsiConsole.MarkupLine(message);
        Console.ReadKey();
    }
}