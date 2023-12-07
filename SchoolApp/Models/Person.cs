using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Person
{
    public int Id { get; set; }

    public long Ssn { get; set; } // TODO: Make varchar

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public virtual Role? Role { get; set; }
}
