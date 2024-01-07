using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Models.DTOs;

namespace SchoolApp.Presenters;

public class AdminPresenter(IAdminService adminService, AdminView adminView, IServiceProvider serviceProvider)
    : IAdminPresenter
{
    private Dictionary<string, Action> _adminChoices = new();
    private AdminDto _adminDto = null!;

    public void HandlePresenter(AdminDto adminDto)
    {
        _adminDto = adminDto;

        var welcomeMessage = $"Signed in as [blue]{_adminDto.Username}[/].";
        var logout = false;

        while (!logout)
        {
            _adminChoices = new Dictionary<string, Action>
            {
                { "Show Staff", OnSelect_ShowStaff },
                { "Show Students", OnSelect_ShowStudents },
                { "Show Students From Year Group", OnSelect_ShowStudentsFromYearGroup },
                { "Show Recent Grades", OnSelect_ShowRecentGrades },
                { "Show Course Statistics", OnSelect_ShowCourseStatistics },
                { "Show Compartment Overview", OnSelect_ShowCompartmentOverview },
                { "Show Active Courses", OnSelect_ShowActiveCourses},
                { "Grade a Student", OnSelect_GradeStudent},
                { "Add New Student", OnSelect_AddNewStudent },
                { "Add New Staff Member\n", OnSelect_AddNewStaffMember },
                { "Logout", () => logout = true },
            };

            RunPresenter(welcomeMessage);
        }
    }

    private void RunPresenter(string welcomeMessage)
    { 
        var choice = adminView.GetChoice(welcomeMessage, _adminChoices.Keys.ToList());

        _adminChoices[choice].Invoke();
    }

    private void OnSelect_ShowStaff()
    {
        var staffRoleNames = adminService.GetStaffRoleNames();
        var selectedRoleNames = adminView.GetStaffRoleNames(staffRoleNames);
        var staff = adminService.GetStaffBasedOnRoles(selectedRoleNames);

        adminView.ShowStaffTable(staff);
    }

    private void OnSelect_ShowStudents()
    {
        var sortByFirstName = adminView.GetSortChoice();
        var orderByDescending = adminView.GetOrderChoice();
        var students = adminService.GetAllStudents(sortByFirstName, orderByDescending);

        adminView.ShowStudentsTable(students);
    }

    private void OnSelect_ShowStudentsFromYearGroup()
    {
        var yearGroups = adminService.GetYearGroups();
        var year = adminView.GetYearGroup(yearGroups);
        var students = adminService.GetStudentsFromYearGroup(year);

        adminView.ShowStudentsFromYearGroupTable(students, year);
    }

    private void OnSelect_ShowRecentGrades()
    {
        var grades = adminService.GetRecentGrades();

        adminView.ShowRecentGradesTable(grades);
    }

    private void OnSelect_ShowCourseStatistics()
    {
        var courses = adminService.GetCourseStatistics();

        adminView.ShowCourseStatistics(courses);
    }

    private void OnSelect_ShowCompartmentOverview()
    {
        var compartments = adminService.GetCompartmentOverview();
        
        adminView.ShowCompartmentOverview(compartments);
    }

    private void OnSelect_ShowActiveCourses()
    {
        var courses = adminService.GetActiveCourses();

        adminView.ShowActiveCourses(courses);
    }

    private void OnSelect_GradeStudent()
    {
        var students = adminService.GetAllStudents(true, true);
        var chosenStudent = adminView.ChooseStudent(students);
        var courses = adminService.GetStudentsCourses(chosenStudent.Ssn);
        var chosenCourse = adminView.ChooseCourse(courses);
        var grades = adminService.GetGradesValues();
        var chosenGrade = adminView.ChooseGrade(grades);
        var teachers = adminService.GetAllTeachers();
        var chosenTeacher = adminView.ChooseTeacher(teachers);

        adminService.GradeStudent(chosenStudent, chosenTeacher, chosenCourse, chosenGrade);
    }

    private void OnSelect_AddNewStudent()
    {
        var addStudentPresenter = serviceProvider.GetRequiredService<AddStudentPresenter>();
        addStudentPresenter.HandlePresenter();
    }

    private void OnSelect_AddNewStaffMember()
    {
        var addStaffPresenter = serviceProvider.GetRequiredService<AddStaffPresenter>();
        addStaffPresenter.HandlePresenter();
    }
}