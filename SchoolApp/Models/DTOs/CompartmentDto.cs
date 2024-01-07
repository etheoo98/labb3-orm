namespace SchoolApp.Models.DTOs;

public class CompartmentDto(string name, decimal salary, int staffCount)
{
    public string Name { get; set; } = name;
    public decimal Salary { get; set; } = salary;
    public int StaffCount { get; set; } = staffCount;
}