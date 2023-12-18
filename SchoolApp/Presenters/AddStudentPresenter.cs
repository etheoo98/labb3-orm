namespace SchoolApp.Presenters;

public class AddStudentPresenter(IAdminService adminService, AddStudentView addStudentView) : IPresenter
{
    private Dictionary<string, Action> _choices = new();
    private string? _ssn;
    private string? _firstName;
    private string? _lastName;
    private string? _gender;
    private int _yearGroup = 1;

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
                { $"Year Group: [blue]{_yearGroup}[/]\n", OnSelect_YearGroup },
                { "Add Student", OnSelect_AddStudent },
                { "Back", () => back = true },
            };

            RunPresenter();
        }
    }

    private void RunPresenter()
    {
        var choice = addStudentView.GetChoice("Please provide the [blue]student's credentials[/].", _choices.Keys.ToList());
        _choices[choice].Invoke();
    }

    private void OnSelect_Ssn()
    {
        _ssn = addStudentView.GetSsn();
    }

    private void OnSelect_FirstName()
    {
        _firstName = addStudentView.GetFirstName();
    }

    private void OnSelect_LastName()
    {
        _lastName = addStudentView.GetLastName();
    }

    private void OnSelect_YearGroup()
    {
        var yearGroups = adminService.GetYearGroups();
        _yearGroup = addStudentView.GetYearGroup(yearGroups);
    }

    private void OnSelect_AddStudent()
    {
        if (!ValidateFields()) return;

        var success = adminService.AddStudent(new Person 
            { 
                Ssn = _ssn!,
                FirstName = _firstName!,
                LastName = _lastName!,
                Gender = _gender!
            }, _yearGroup);
        
        HandleServiceResponse(success);
    }

    private bool ValidateFields()
    {
        if (string.IsNullOrEmpty(_ssn) || string.IsNullOrEmpty(_firstName) || string.IsNullOrEmpty(_lastName)
            || _ssn.Length != 10 || !int.TryParse(_ssn, out _))
        {
            addStudentView.ShowMissingFieldsMessage();
            
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
            addStudentView.ShowServiceSuccessMessage();
        }
        else
        {
            addStudentView.ShowServiceFailedMessage();
        }
    }
}