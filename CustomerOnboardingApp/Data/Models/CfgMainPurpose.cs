using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class CfgMainPurpose
{
    public int MainPurposeId { get; set; }

    public string PurposeName { get; set; } = null!;

    public virtual ICollection<TblCustomer> TblCustomers { get; set; } = new List<TblCustomer>();
}
