using System.Globalization;
using SchoolApp.Models.DTOs;
using Spectre.Console;

namespace SchoolApp.Views;

public class AdminView : View
{
    public List<T> GetStaffRoleNames<T>(List<T> staffRoleNames) where T : notnull =>
        GetMultiChoice($"What staff [{HighlightColor}]roles[/] do you want to show?", staffRoleNames);

    public void ShowStaffTable(List<StaffDto> staff)
    {
        var headers = new List<string> { "First Name", "Last Name", "Role" };
        var rows = staff.Select(s => new List<string> { s.FirstName, s.LastName, s.Role }).ToList();
        var table = new TableData("Staff", "These are all of the staff members based on your filters.", headers, rows);

        ShowTable(table);
    }

    public bool GetSortChoice()
    {
        Dictionary<string, bool> sortChoices = new()
        {
            { "Sort by first name", true },
            { "Sort by last name", false },
        };

        var choice = GetChoice("What do you want to sort the Students by?", sortChoices.Keys.ToList());

        return sortChoices[choice];
    }

    public bool GetOrderChoice()
    {
        Dictionary<string, bool> orderChoices = new()
        {
            { "Order by ascending (A-Z)", false },
            { "Order by descending (Z-A)", true },
        };

        var choice = GetChoice("What do you want to order the Students by?", orderChoices.Keys.ToList());

        return orderChoices[choice];
    }

    public void ShowStudentsTable(List<StudentDto> students)
    {
        var headers = new List<string> { "First Name", "Last Name", "Year Group", "Course Count" };
        var rows = students.Select(s => new List<string> { s.FirstName, s.LastName, s.YearGroup.ToString(), s.CourseCount.ToString() }).ToList();
        var table = new TableData("Students", $"These are all of the students.\nIn total there are {students.Count} students.", headers, rows);

        ShowTable(table);
    }

    public int GetYearGroup(List<int> yearGroups) => GetChoice("What year group?", yearGroups);

    public void ShowStudentsFromYearGroupTable(List<StudentDto> students, int year)
    {
        var headers = new List<string> { "First Name", "Last name" };
        var rows = students.Select(s => new List<string> { s.FirstName, s.LastName }).ToList();
        var table = new TableData("Students", $"These are all of the students in year {year}\n In total there are {students.Count} students", headers, rows);

        ShowTable(table);
    }

    public void ShowRecentGradesTable(List<GradeDto> grades)
    {
        var headers = new List<string> { "SSN", "Last Name", "Course", "Grade", "Date" };
        var rows = grades.Select(g => new List<string> { g.Ssn, g.LastName, g.Course, g.Grade, g.Date.ToShortDateString() }).ToList();
        var table = new TableData("Recent Grades", "These are the grades set during the past year.", headers, rows);

        ShowTable(table);
    }

    public void ShowCourseStatistics(List<CourseDto> courses)
    {
        var headers = new List<string> { "Name", "Highest Grade", "Average Grade", "Lowest Grade" };
        var rows = courses.Select(c => new List<string> { c.Course, c.HighestGrade, c.AverageGrade, c.LowestGrade }).ToList();
        var table = new TableData("Course Statistics", "These are the grades set during the past year.", headers, rows);

        ShowTable(table);
    }

    public void ShowCompartmentOverview(List<CompartmentDto> compartmentsList)
    {
        var headers = new List<string> { "Name", "Salary", "Staff Count" };
        var rows = compartmentsList.Select(c => new List<string> { c.Name, c.Salary.ToString(CultureInfo.CurrentCulture), c.StaffCount.ToString() }).ToList();
        var totalStaffCount = compartmentsList.Sum(compartment => compartment.StaffCount);
        var averageSalary = Math.Round(compartmentsList.Average(compartments => compartments.Salary));
        var table = new TableData("Compartment Overview", $"These are all the compartments.\nIn total there are {totalStaffCount} staff with the average salary of {averageSalary}", headers, rows);
        
        ShowTable(table);
    }

    public void ShowActiveCourses(List<ActiveCourseDto> courses)
    {
        var headers = new List<string> { "Name", "Teacher", "Student Count" };
        var rows = courses.Select(c => new List<string> { c.Name, c.Teachers, c.StudentCount.ToString() }).ToList();
        var table = new TableData("Active Courses", $"These are the active courses.\nIn total there are {courses.Count} courses.", headers, rows);
        
        ShowTable(table);
    }

    public StudentDto ChooseStudent(List<StudentDto> students)
    {
        List<string> studentInfoList = [];

        foreach (var student in students)
        {
            var info = $"{student.FirstName} {student.LastName} ({student.Ssn})";
            studentInfoList.Add(info);
        }

        var choice = GetChoice("What student do you want to grade?", studentInfoList);

        return students.FirstOrDefault(s => s.Ssn == choice.Split('(', ')')[1].Trim())!;
    }

    public Course ChooseCourse(List<Course> courses)
    {
        List<string> courseNames = [];
        
        foreach (var course in courses)
        {
            courseNames.Add(course.Name);
        }

        var choice = GetChoice("What course do you want to grade them in?", courseNames);

        return courses.Find(c => c.Name == choice)!;
    }

    public GradeValue ChooseGrade(List<GradeValue> grades)
    {
        List<string> gradeLetters = [];
        
        foreach (var grade in grades)
        {
            gradeLetters.Add(grade.Letter);
        }

        var choice = GetChoice("What grade do you want to give them?", gradeLetters);

        return grades.Find(g => g.Letter == choice)!;
    }

    public TeacherDto ChooseTeacher(List<TeacherDto> teachers)
    {
        List<string> studentInfoList = [];

        foreach (var teacher in teachers)
        {
            var info = $"{teacher.FirstName} {teacher.LastName} ({teacher.Ssn})";
            studentInfoList.Add(info);
        }

        var choice = GetChoice("What student do you want to grade?", studentInfoList);

        return teachers.FirstOrDefault(s => s.Ssn == choice.Split('(', ')')[1].Trim())!;
    }

    private List<T> GetMultiChoice<T>(string prompt, List<T> choices) where T : notnull
    {
        Console.Clear();
        
        return AnsiConsole.Prompt(new MultiSelectionPrompt<T>()
            .HighlightStyle($"{HighlightColor}")
            .Title(prompt)
            .Required()
            .PageSize(10)
            .AddChoices(choices));
    }

    private void ShowTable(TableData data)
    {
        var table = new Table();
        
        table.BorderColor(data.BorderColor);
        table.Border(data.TableBorder);
        table.Title(data.Title);
        table.Caption(data.Caption);

        foreach (var header in data.Headers)
        {
            table.AddColumn($"[{data.ColumnColor}]{header}[/]");
        }

        table.Expand();

        foreach (var row in data.Rows)
        {
            table.AddRow(row.ToArray());
        }

        Console.Clear();
        AnsiConsole.Write(table);
        Console.ReadKey();
    }
}