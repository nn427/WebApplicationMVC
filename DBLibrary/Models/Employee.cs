using System;
using System.Collections.Generic;

namespace DBLibrary.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? EmpId { get; set; }

    public string? Name { get; set; }

    public int? Salary { get; set; }
}
