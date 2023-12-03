using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Role
{
    public int Id { get; set; }

    public int PersonsId { get; set; }

    public virtual Person Persons { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
