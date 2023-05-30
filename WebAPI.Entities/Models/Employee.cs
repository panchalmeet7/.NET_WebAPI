using System;
using System.Collections.Generic;

namespace WebAPI.Entities.Models;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Addresss { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string City { get; set; } = null!;
}
