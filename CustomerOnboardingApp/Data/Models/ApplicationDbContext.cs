using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerOnboardingApp.Data.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CfgBusinessActivity> CfgBusinessActivities { get; set; }

    public virtual DbSet<CfgCountry> CfgCountries { get; set; }

    public virtual DbSet<CfgMainPurpose> CfgMainPurposes { get; set; }

    public virtual DbSet<CfgTypeOfEntity> CfgTypeOfEntities { get; set; }

    public virtual DbSet<CfgUser> CfgUsers { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblDirectorShareholder> TblDirectorShareholders { get; set; }

    public virtual DbSet<TblDocument> TblDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CustomerOnboardingDB;Trusted_Connection=True;User ID=your_user;Password=your_password;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CfgBusinessActivity>(entity =>
        {
            entity.HasKey(e => e.BusinessActivityId).HasName("PK__CFG_Busi__50EF702EAFEA41F2");

            entity.ToTable("CFG_BusinessActivity");

            entity.Property(e => e.BusinessActivityName).HasMaxLength(50);
        });

        modelBuilder.Entity<CfgCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__CFG_Coun__10D1609F190F62A7");

            entity.ToTable("CFG_Country");

            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<CfgMainPurpose>(entity =>
        {
            entity.HasKey(e => e.MainPurposeId).HasName("PK__CFG_Main__B0AB6E77CF0C9F1E");

            entity.ToTable("CFG_MainPurpose");

            entity.Property(e => e.PurposeName).HasMaxLength(255);
        });

        modelBuilder.Entity<CfgTypeOfEntity>(entity =>
        {
            entity.HasKey(e => e.TypeOfEntityId).HasName("PK__CFG_Type__F73E57A0400C4BCC");

            entity.ToTable("CFG_TypeOfEntity");

            entity.Property(e => e.EntityTypeName).HasMaxLength(150);
        });

        modelBuilder.Entity<CfgUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cfg_User__3214EC07C8081E57");

            entity.ToTable("Cfg_User");

            entity.HasIndex(e => e.Email, "UQ__Cfg_User__A9D10534416FF683").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Tbl_Cust__A4AE64D8049CBCE4");

            entity.ToTable("Tbl_Customer");

            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.DesignatedApplicantFirstName).HasMaxLength(100);
            entity.Property(e => e.DesignatedApplicantLastName).HasMaxLength(100);
            entity.Property(e => e.DesignatedApplicantTitle).HasMaxLength(10);
            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.LicenseRequirement).HasMaxLength(255);
            entity.Property(e => e.RegistrationNumber).HasMaxLength(50);

            entity.HasOne(d => d.BusinessActivity).WithMany(p => p.TblCustomers)
                .HasForeignKey(d => d.BusinessActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessActivity");

            entity.HasOne(d => d.Country).WithMany(p => p.TblCustomers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country");

            entity.HasOne(d => d.MainPurpose).WithMany(p => p.TblCustomers)
                .HasForeignKey(d => d.MainPurposeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MainPurpose");

            entity.HasOne(d => d.TypeOfEntity).WithMany(p => p.TblCustomers)
                .HasForeignKey(d => d.TypeOfEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TypeOfEntity");
        });

        modelBuilder.Entity<TblDirectorShareholder>(entity =>
        {
            entity.HasKey(e => e.DirectorShareholderId).HasName("PK__Tbl_Dire__46CA5EAC7F3F3F0C");

            entity.ToTable("Tbl_DirectorShareholder");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PassportNumber).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(10);

            entity.HasOne(d => d.Customer).WithMany(p => p.TblDirectorShareholders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_DirectorShareholder");
        });

        modelBuilder.Entity<TblDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Tbl_Docu__1ABEEF0FA378EDC4");

            entity.ToTable("Tbl_Document");

            entity.Property(e => e.DocumentName).HasMaxLength(255);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.TblDocuments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Document");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
