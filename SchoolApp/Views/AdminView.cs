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
            { "Sort by first name", true }, { "Sort by last name", false },
        };

        var choice = GetChoice("What do you want to sort the Students by?", sortChoices.Keys.ToList());

        return sortChoices[choice];
    }

    public bool GetOrderChoice()
    {
        Dictionary<string, bool> orderChoices = new()
        {
            { "Order by ascending (A-Z)", false }, { "Order by descending (Z-A)", true },
        };

        var choice = GetChoice("What do you want to order the Students by?", orderChoices.Keys.ToList());

        return orderChoices[choice];
    }

    public void ShowStudentsTable(List<StudentDto> students)
    {
        var headers = new List<string> { "First Name", "Last Name", "Year Group", "Course Count" };
        var rows = students.Select(s =>
                new List<string> { s.FirstName, s.LastName, s.YearGroup.ToString(), s.CourseCount.ToString() })
            .ToList();
        var table = new TableData("Students",
            $"These are all of the students.\nIn total there are {students.Count} students.", headers, rows);

        ShowTable(table);
    }

    public int GetYearGroup(List<int> yearGroups) => GetChoice("What year group?", yearGroups);

    public void ShowStudentsFromYearGroupTable(List<StudentDto> students, int year)
    {
        var headers = new List<string> { "First Name", "Last name" };
        var rows = students.Select(s => new List<string> { s.FirstName, s.LastName }).ToList();
        var table = new TableData("Students",
            $"These are all of the students in year {year}\n In total there are {students.Count} students", headers,
            rows);

        ShowTable(table);
    }

    public void ShowRecentGradesTable(List<GradeDto> grades)
    {
        var headers = new List<string>
        {
            "SSN",
            "Last Name",
            "Course",
            "Grade",
            "Date"
        };
        var rows = grades.Select(g => new List<string>
            {
                g.Ssn,
                g.LastName,
                g.Course,
                g.Grade,
                g.Date.ToShortDateString()
            })
            .ToList();
        var table = new TableData("Recent Grades", "These are the grades set during the past year.", headers, rows);

        ShowTable(table);
    }

    public void ShowCourseStatistics(List<CourseDto> courses)
    {
        var headers = new List<string> { "Name", "Highest Grade", "Average Grade", "Lowest Grade" };
        var rows = courses.Select(c => new List<string> { c.Course, c.HighestGrade, c.AverageGrade, c.LowestGrade })
            .ToList();
        var table = new TableData("Course Statistics", "These are the grades set during the past year.", headers, rows);

        ShowTable(table);
    }

    private List<T> GetMultiChoice<T>(string prompt, List<T> choices) where T : notnull
    {
        Console.Clear();
        
        return AnsiConsole.Prompt(new MultiSelectionPrompt<T>().HighlightStyle($"{HighlightColor}")
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