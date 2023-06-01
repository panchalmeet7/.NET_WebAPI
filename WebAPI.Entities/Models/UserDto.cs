using System;
using System.Collections.Generic;

namespace WebAPI.Entities.Models;

public partial class UserDto
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
