namespace SchoolApp.Views;

public class AddStaffView : View
{
    public string GetSsn() => GetStringInput($"Enter [{HighlightColor}]SSN[/]:");
    public string GetFirstName() => GetStringInput($"What is the staff's [{HighlightColor}]first name[/]?");
    public string GetLastName() => GetStringInput($"What is the staff's [{HighlightColor}]last name[/]?");
    public string GetRole(List<string> roles) => GetChoice($"What [{HighlightColor}]role[/] does the staff have?", roles);
    public string GetCompartment(List<string> compartments) =>
        GetChoice($"What [{HighlightColor}]compartment[/] does the staff belong to?", compartments);
    public string GetAdminUsername() => GetStringInput($"What should the administrator's [{HighlightColor}]username[/] be?");
    public string GetAdminPassword() => GetSecretStringInput($"What should the administrator's [{HighlightColor}]password[/] be?");

    public void ShowMissingFieldsMessage()
    {
        ShowMessage($"You must fill out [{HighlightColor}]all[/] fields! Please also make sure you've used proper formatting.");
    }
    
    public void ShowServiceSuccessMessage()
    {
        ShowMessage("Staff successfully added!");
    }
    
    public void ShowServiceFailedMessage()
    {
        ShowMessage("[red]Failed[/] to add staff!");
    }
}