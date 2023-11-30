using SchoolApp.Models.DTOs;

namespace SchoolApp.Presenters;

public class AdminPresenter(AdminDto adminDto, IAdminService adminService, AdminView adminView) : IPresenter
{
    private Dictionary<string, Action> _adminChoices = new();
    
    public void HandlePresenter()
    {
        var prompt = $"Welcome back {adminDto.Username}!";
        var logout = false;

        while (!logout)
        {
            _adminChoices = new Dictionary<string, Action>
            {
                {"Show Staff", OnSelect_ShowStaff},
                {"Show Students", OnSelect_ShowStudents},
                {"Show Students From Class", OnSelect_ShowStudentsFromClass},
                {"Show Recent Grades", OnSelect_ShowRecentGrades},
                {"Show Grade Statistics", OnSelect_ShowGradeStatistics},
                {"Add New Student", OnSelect_AddNewStudent},
                {"Add New Staff Member", OnSelect_AddNewStaffMember},
                {"Exit", () => logout = true},
            };
            
            Console.Clear();

            var choice = adminView.GetChoice(prompt, _adminChoices.Keys.ToList());
            
            _adminChoices[choice].Invoke();
        }
    }

    private void OnSelect_ShowStaff()
    {
        throw new NotImplementedException();
    }
    
    private void OnSelect_ShowStudents()
    {
        throw new NotImplementedException();
    }
    
    private void OnSelect_ShowStudentsFromClass()
    {
        throw new NotImplementedException();
    }
    
    private void OnSelect_ShowRecentGrades()
    {
        throw new NotImplementedException();
    }
    
    private void OnSelect_ShowGradeStatistics()
    {
        throw new NotImplementedException();
    }
    
    private void OnSelect_AddNewStudent()
    {
        throw new NotImplementedException();
    }
    
    private void OnSelect_AddNewStaffMember()
    {
        throw new NotImplementedException();
    }
}