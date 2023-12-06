using SchoolApp.Models.DTOs;
using Spectre.Console;

namespace SchoolApp.Views;

public class AdminView : View
{
    public void ShowStaff(List<StaffDto> staffList)
    {
        foreach (var staff in staffList)
        {
            Console.WriteLine($"{staff.FirstName} {staff.LastName} {staff.Role}");
        }

        Console.ReadKey();
    }
    
    public void ShowStudents(List<StudentDto> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName}");
        }

        Console.ReadKey();
    }

    public void ShowStudentsFromYearGroup(List<StudentDto> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName}");
        }

        Console.ReadKey();
    }

    public void ShowCourseStatistics(List<CourseDto> courses)
    {
        foreach (var course in courses)
        {
            Console.WriteLine($"Name: {course.Course} | Highest: {course.HighestGrade} | Average: {course.AverageGrade} | Lowest: {course.LowestGrade}");
        }
        
        Console.ReadKey();
    }
    
    public void ShowRecentGrades(List<GradeDto> grades)
    {
        foreach (var grade in grades)
        {
            Console.WriteLine($"Name: {grade.FirstName} | Course: {grade.Course} | Date: {grade.Date}");
        }
        
        Console.ReadKey();
    }
}