namespace SchoolApp.Presenters;

public class AddStaffPresenter(IAdminService adminService, AddStaffView addStaffView) : IPresenter
{
    private Dictionary<string, Action> _choices = new();
    private string? _ssn;
    private string? _firstName;
    private string? _lastName;
    private string? _gender;
    private string? _role;
    private string? _compartment;

    public void HandlePresenter()
    {
        var back = false;

        while (!back)
        {
            _choices = new Dictionary<string, Action>
            {
                { $"SSN: [blue]{_ssn}[/]", OnSelect_Ssn },
                { $"First Name: [blue]{_firstName}[/]", OnSelect_FirstName },
                { $"Last Name: [blue]{_lastName}[/]", OnSelect_LastName },
                { $"Role: [blue]{_role}[/]", OnSelect_Role },
                { $"Compartment: [blue]{_compartment}[/]\n", OnSelect_Compartment },
                { "Add Staff", OnSelect_AddStaff },
                { "Back", () => back = true },
            };

            RunPresenter();
        }
    }

    private void RunPresenter()
    {
        var choice = addStaffView.GetChoice("Please provide the [blue]staff's credentials[/].", _choices.Keys.ToList());
        _choices[choice].Invoke();
    }

    private void OnSelect_Ssn()
    {
        _ssn = addStaffView.GetSsn();
    }

    private void OnSelect_FirstName()
    {
        _firstName = addStaffView.GetFirstName();
    }

    private void OnSelect_LastName()
    {
        _lastName = addStaffView.GetLastName();
    }

    private void OnSelect_Role()
    {
        var roles = adminService.GetStaffRoleNames();
        _role = addStaffView.GetRole(roles);
    }

    private void OnSelect_Compartment()
    {
        var compartments = adminService.GetCompartments();
        _compartment = addStaffView.GetCompartment(compartments);
    }

    private void OnSelect_AddStaff()
    {
        if (!ValidateFields()) return;

        var person = new Person 
            { 
                Ssn = _ssn!,
                FirstName = _firstName!,
                LastName = _lastName!,
                Gender = _gender!
            };
        
        var success = false;

        switch (_role)
        {
            case "Administrator":
                var username = addStaffView.GetAdminUsername();
                var password = addStaffView.GetAdminPassword();
                success = adminService.AddAdministrator(person, _role, _compartment!, username, password);
                break;
            case "Principal":
                success = adminService.AddPrincipal(person, _role, _compartment!);
                break;
            case "Teacher":
                success = adminService.AddTeacher(person, _role, _compartment!);
                break;
        }

        HandleServiceResponse(success);
    }
    
    private bool ValidateFields()
    {
        if (string.IsNullOrEmpty(_ssn) || string.IsNullOrEmpty(_firstName) || string.IsNullOrEmpty(_lastName)
            || string.IsNullOrEmpty(_role) || string.IsNullOrEmpty(_compartment) || _ssn.Length != 10 || !long.TryParse(_ssn, out _))
        {
            addStaffView.ShowMissingFieldsMessage();
            
            return false;
        }
        
        SetGenderBasedOnSsn();
        FormatSsn();

        return true;
    }

    private void SetGenderBasedOnSsn()
    {
        var genderNum = (int)_ssn![6];
        
        _gender = genderNum % 2 == 0 ? "Female" : "Male";
    }

    private void FormatSsn()
    {
        _ssn = _ssn![..6] + "-" + _ssn[6..];
    }

    private void HandleServiceResponse(bool success)
    {
        if (success)
        {
            addStaffView.ShowServiceSuccessMessage();
        }
        else
        {
            addStaffView.ShowServiceFailedMessage();
        }
    }
}