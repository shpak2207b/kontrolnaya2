using System;
using System.Collections.Generic;

namespace kontrolnaya.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public DateOnly? Date { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
