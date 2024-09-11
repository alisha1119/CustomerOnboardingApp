using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class TblDirectorShareholder
{
    public int DirectorShareholderId { get; set; }

    public int CustomerId { get; set; }

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string? Role { get; set; }

    public virtual TblCustomer Customer { get; set; } = null!;
}
