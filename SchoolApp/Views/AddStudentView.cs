namespace SchoolApp.Views;

public class AddStudentView : View
{
    public string GetSsn() => GetStringInput($"What is the student's [{HighlightColor}]Swedish Social Security Number[/] (numbers only)?");
    public string GetFirstName() => GetStringInput($"What is the student's [{HighlightColor}]first name[/]?");
    public string GetLastName() => GetStringInput($"What is the student's [{HighlightColor}]last name[/]?");
    public int GetYearGroup(List<int> yearGroups) => GetChoice($"In what [{HighlightColor}]year group[/] is the student in?", yearGroups);

    public void ShowMissingFieldsMessage()
    {
        ShowMessage($"You must fill out [{HighlightColor}]all[/] fields! Please also make sure you've used proper formatting.");
    }
    
    public void ShowServiceSuccessMessage()
    {
        ShowMessage("Student [green]successfully[/] added!");
    }
    
    public void ShowServiceFailedMessage()
    {
        ShowMessage("[red]Failed[/] to add Student!");
    }
}