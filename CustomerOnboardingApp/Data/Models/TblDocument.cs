using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class TblDocument
{
    public int DocumentId { get; set; }

    public int CustomerId { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentType { get; set; }

    public virtual TblCustomer Customer { get; set; } = null!;
}
