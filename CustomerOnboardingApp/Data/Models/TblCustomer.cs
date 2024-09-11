using System;
using System.Collections.Generic;

namespace CustomerOnboardingApp.Data.Models;

public partial class TblCustomer
{
    public int CustomerId { get; set; }

    public int MainPurposeId { get; set; }

    public string CompanyName { get; set; } = null!;

    public int TypeOfEntityId { get; set; }

    public int BusinessActivityId { get; set; }

    public string? LicenseRequirement { get; set; }

    public int CountryId { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public DateOnly DateOfIncorporation { get; set; }

    public string DesignatedApplicantTitle { get; set; } = null!;

    public string DesignatedApplicantFirstName { get; set; } = null!;

    public string DesignatedApplicantLastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public bool? IsProcessed { get; set; }

    public bool? IsApproved { get; set; }

    public virtual CfgBusinessActivity BusinessActivity { get; set; } = null!;

    public virtual CfgCountry Country { get; set; } = null!;

    public virtual CfgMainPurpose MainPurpose { get; set; } = null!;

    public virtual ICollection<TblDirectorShareholder> TblDirectorShareholders { get; set; } = new List<TblDirectorShareholder>();

    public virtual ICollection<TblDocument> TblDocuments { get; set; } = new List<TblDocument>();

    public virtual CfgTypeOfEntity TypeOfEntity { get; set; } = null!;
}
