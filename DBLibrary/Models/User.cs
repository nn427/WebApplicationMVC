using System;
using System.Collections.Generic;

namespace DBLibrary.Models;

public partial class User
{
    public int Id { get; set; }

    public string UId { get; set; } = null!;

    public string NickName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string Username { get; set; } = null!;
}
