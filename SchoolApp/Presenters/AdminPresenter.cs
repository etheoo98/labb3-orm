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
                {"Show Staff*", OnSelect_ShowStaff},
                {"Show Students*", OnSelect_ShowStudents},
                {"Show Students* From Year Group*", OnSelect_ShowStudentsFromYearGroup},
                {"Show Recent Grades*", OnSelect_ShowRecentGrades},
                {"Show Course Statistics*", OnSelect_ShowCourseStatistics},
                {"Add New Student", OnSelect_AddNewStudent},
                {"Add New Staff Member\n", OnSelect_AddNewStaffMember},
                {"Exit", () => logout = true},
            };
            
            Console.Clear();

            var choice = adminView.GetChoice(prompt, _adminChoices.Keys.ToList());
            
            _adminChoices[choice].Invoke();
        }
    }

    private void OnSelect_ShowStaff()
    {
        Dictionary<string, string> staffRoleChoices = new()
        {
            {"Include Administrators", "Administrator" },
            {"Include Principals", "Principal"},
            {"Include Teachers", "Teacher"},
        };

        var options = adminView.GetMultiChoice("What Staff do you want to show?", staffRoleChoices.Keys.ToList());
        var roles = options.Select(option => staffRoleChoices[option]).ToList();
        var staff = adminService.GetStaffBasedOnRoles(roles);

        adminView.ShowStaff(staff);
    }
    
    private void OnSelect_ShowStudents()
    {
        Dictionary<string, bool> sortChoices = new()
        {
            {"Sort by first name", true},
            {"Sort by last name", false},
        };
        
        Dictionary<string, bool> orderChoices = new()
        {
            {"Order by ascending (A-Z)", false},
            {"Order by descending (Z-A)", true},
        };
        
        var sort = adminView
            .GetChoice("What do you want to sort the Students by?", sortChoices.Keys.ToList());
        
        var order = adminView
            .GetChoice("How do you want to order the Students?", orderChoices.Keys.ToList());
        
        var students = adminService
            .GetAllStudents(sortChoices[sort], orderChoices[order]);
        
        adminView.ShowStudents(students);
    }
    
    private void OnSelect_ShowStudentsFromYearGroup()
    {
        var yearGroups = adminService.GetYearGroups();
        var year = adminView
            .GetChoice("What year group?", yearGroups.Select(y => y.Year).ToList());
        var students = adminService.GetStudentsFromYearGroup(year);
        
        adminView.ShowStudentsFromYearGroup(students);
    }
    
    private void OnSelect_ShowRecentGrades()
    {
        var grades = adminService.GetRecentGrades();
        adminView.ShowRecentGrades(grades);
    }
    
    private void OnSelect_ShowCourseStatistics()
    {
        var courses = adminService.GetCourseStatistics();
        adminView.ShowCourseStatistics(courses);
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