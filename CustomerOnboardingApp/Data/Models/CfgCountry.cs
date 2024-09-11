using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class CfgCountry
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<TblCustomer> TblCustomers { get; set; } = new List<TblCustomer>();
}
