using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class CfgTypeOfEntity
{
    public int TypeOfEntityId { get; set; }

    public string EntityTypeName { get; set; } = null!;

    public virtual ICollection<TblCustomer> TblCustomers { get; set; } = new List<TblCustomer>();
}
