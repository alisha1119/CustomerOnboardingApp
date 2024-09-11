using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOnboardingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CFG_BusinessActivity",
                columns: table => new
                {
                    BusinessActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessActivityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CFG_Busi__50EF702EAFEA41F2", x => x.BusinessActivityId);
                });

            migrationBuilder.CreateTable(
                name: "CFG_Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CFG_Coun__10D1609F190F62A7", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CFG_MainPurpose",
                columns: table => new
                {
                    MainPurposeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurposeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CFG_Main__B0AB6E77CF0C9F1E", x => x.MainPurposeId);
                });

            migrationBuilder.CreateTable(
                name: "CFG_TypeOfEntity",
                columns: table => new
                {
                    TypeOfEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityTypeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CFG_Type__F73E57A0400C4BCC", x => x.TypeOfEntityId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPurposeId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeOfEntityId = table.Column<int>(type: "int", nullable: false),
                    BusinessActivityId = table.Column<int>(type: "int", nullable: false),
                    LicenseRequirement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfIncorporation = table.Column<DateOnly>(type: "date", nullable: false),
                    DesignatedApplicantTitle = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignatedApplicantFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DesignatedApplicantLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Cust__A4AE64D8820A2F12", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_BusinessActivity",
                        column: x => x.BusinessActivityId,
                        principalTable: "CFG_BusinessActivity",
                        principalColumn: "BusinessActivityId");
                    table.ForeignKey(
                        name: "FK_Country",
                        column: x => x.CountryId,
                        principalTable: "CFG_Country",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_MainPurpose",
                        column: x => x.MainPurposeId,
                        principalTable: "CFG_MainPurpose",
                        principalColumn: "MainPurposeId");
                    table.ForeignKey(
                        name: "FK_TypeOfEntity",
                        column: x => x.TypeOfEntityId,
                        principalTable: "CFG_TypeOfEntity",
                        principalColumn: "TypeOfEntityId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DirectorShareholder",
                columns: table => new
                {
                    DirectorShareholderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Dire__46CA5EACC161E26F", x => x.DirectorShareholderId);
                    table.ForeignKey(
                        name: "FK_Customer_DirectorShareholder",
                        column: x => x.CustomerId,
                        principalTable: "Tbl_Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_BusinessActivityId",
                table: "Tbl_Customer",
                column: "BusinessActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_CountryId",
                table: "Tbl_Customer",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_MainPurposeId",
                table: "Tbl_Customer",
                column: "MainPurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_TypeOfEntityId",
                table: "Tbl_Customer",
                column: "TypeOfEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DirectorShareholder_CustomerId",
                table: "Tbl_DirectorShareholder",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_DirectorShareholder");

            migrationBuilder.DropTable(
                name: "Tbl_Customer");

            migrationBuilder.DropTable(
                name: "CFG_BusinessActivity");

            migrationBuilder.DropTable(
                name: "CFG_Country");

            migrationBuilder.DropTable(
                name: "CFG_MainPurpose");

            migrationBuilder.DropTable(
                name: "CFG_TypeOfEntity");
        }
    }
}
