using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class CfgBusinessActivity
{
    public int BusinessActivityId { get; set; }

    public string BusinessActivityName { get; set; } = null!;

    public virtual ICollection<TblCustomer> TblCustomers { get; set; } = new List<TblCustomer>();
}
