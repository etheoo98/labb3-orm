using SchoolApp.Models.DTOs;
using Spectre.Console;

namespace SchoolApp.Views;

public class AdminView : View
{
    public void ShowStaff(List<StaffDto> staffList)
    {
        var table = new Table();
        table.BorderColor(Color.SpringGreen3);
        table.Border(TableBorder.MinimalHeavyHead);
        table.Title("[dodgerblue1]Staff[/]");
        table.Caption("These are all of the staff members based on your filters.");
        table.AddColumn("[gold1]First Name[/]");
        table.AddColumn("[gold1]Last Name[/]");
        table.AddColumn("[gold1]Role[/]");
        table.Expand();
        
        foreach (var staff in staffList)
        {
            table.AddRow(staff.FirstName, staff.LastName, staff.Role);
        }
        
        AnsiConsole.Write(table);

        Console.ReadKey();
    }
    
    public void ShowStudents(List<StudentDto> students)
    {
        var table = new Table();
        table.BorderColor(Color.SpringGreen3);
        table.Border(TableBorder.MinimalHeavyHead);
        table.Title("[dodgerblue1]Students[/]");
        table.Caption($"These are all of the students.\nIn total there are {students.Count} students.");
        table.AddColumn("[gold1]First Name[/]");
        table.AddColumn("[gold1]Last Name[/]");
        table.AddColumn("[gold1]Year Group[/]");
        table.AddColumn("[gold1]Course Count[/]");
        table.Expand();
        
        foreach (var student in students)
        {
            table.AddRow(
                student.FirstName,
                student.LastName,
                student.YearGroup.ToString(),
                student.CourseCount.ToString());
        }
        
        AnsiConsole.Write(table);

        Console.ReadKey();
    }

    public void ShowStudentsFromYearGroup(List<StudentDto> students)
    {
        var table = new Table();
        table.BorderColor(Color.SpringGreen3);
        table.Border(TableBorder.MinimalHeavyHead);
        table.Title("[dodgerblue1]Students[/]");
        table.Caption($"These are all of the students in year {students.First().YearGroup}" +
                      $"\nIn total there are {students.Count} students.");
        table.AddColumn("[gold1]First Name[/]");
        table.AddColumn("[gold1]Last Name[/]");
        table.Expand();
        
        foreach (var student in students)
        {
            table.AddRow(student.FirstName, student.LastName);
        }
        
        AnsiConsole.Write(table);

        Console.ReadKey();
    }

    public void ShowCourseStatistics(List<CourseDto> courses)
    {
        var table = new Table();
        table.BorderColor(Color.SpringGreen3);
        table.Border(TableBorder.MinimalHeavyHead);
        table.Title("[dodgerblue1]Course Statistics[/]");
        table.Caption("These statistics are based upon all grades for the particular course.");
        table.AddColumn("[gold1]Name[/]");
        table.AddColumn("[gold1]Highest Grade[/]");
        table.AddColumn("[gold1]Average Grade[/]");
        table.AddColumn("[gold1]Lowest Grade[/]");
        table.Expand();
        
        foreach (var course in courses)
        {
            table.AddRow(
                course.Course,
                course.HighestGrade,
                course.AverageGrade,
                course.LowestGrade);
        }
        
        AnsiConsole.Write(table);

        Console.ReadKey();
    }
    
    public void ShowRecentGrades(List<GradeDto> grades)
    {
        var table = new Table();
        table.BorderColor(Color.SpringGreen3);
        table.Border(TableBorder.MinimalHeavyHead);
        table.Title("[dodgerblue1]Recent Grades[/]");
        table.Caption("These are the grades set during the past year.");
        table.AddColumn("[gold1]SSN[/]");
        table.AddColumn("[gold1]Last Name[/]");
        table.AddColumn("[gold1]Course[/]");
        table.AddColumn("[gold1]Grade[/]");
        table.AddColumn("[gold1]Date[/]");
        table.Expand();
        
        foreach (var grade in grades)
        {
            table.AddRow(
                grade.Ssn.ToString(),
                grade.LastName,
                grade.Course,
                grade.Grade,
                grade.Date.ToShortDateString());
        }
        
        AnsiConsole.Write(table);

        Console.ReadKey();
    }
}