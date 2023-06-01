using System;
using System.Collections.Generic;

namespace WebAPI.Entities.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;
}
