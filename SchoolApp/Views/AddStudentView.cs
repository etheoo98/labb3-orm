namespace SchoolApp.Views;

public class AddStudentView : View
{
    public string GetSsn() => GetStringInput($"What is the student's [{HighlightColor}]Social Security Number[/]?");
    public string GetFirstName() => GetStringInput($"What is the student's [{HighlightColor}]first name[/]?");
    public string GetLastName() => GetStringInput($"What is the student's [{HighlightColor}]last name[/]?");
    public string GetGender() => GetStringInput($"What is the student's [{HighlightColor}]gender[/]?");
    public int GetYearGroup(List<int> yearGroups) => GetChoice($"In what [{HighlightColor}]year group[/] is the student in?", yearGroups);
    
    public void ShowServiceSuccessMessage()
    {
        ShowMessage("Student successfully added!");
    }
    
    public void ShowServiceFailedMessage()
    {
        ShowMessage("[red]Failed[/] to add Student!");
    }
}