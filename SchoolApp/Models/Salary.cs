namespace SchoolApp.Models;

public partial class Salary
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public virtual ICollection<Compartment> Compartments { get; set; } = new List<Compartment>();
}
