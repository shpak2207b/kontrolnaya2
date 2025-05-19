using System;
using System.Collections.Generic;

namespace kontrolnaya.Models;

public partial class Request
{
    public int Id { get; set; }

    public string? RequestNumber { get; set; }

    public string? Title { get; set; }

    public string? Type { get; set; }

    public string? ProblemDescription { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int Status { get; set; }

    public int? MasterId { get; set; }

    public virtual User? Master { get; set; }
}
