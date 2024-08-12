using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sonaar.Service.APi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactPrifix = table.Column<string>(type: "TEXT", nullable: true),
                    ContactFirstName = table.Column<string>(type: "TEXT", nullable: true),
                    ContactLastName = table.Column<string>(type: "TEXT", nullable: true),
                    ContactPhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ContactAddress1 = table.Column<string>(type: "TEXT", nullable: true),
                    ContactAddress2 = table.Column<string>(type: "TEXT", nullable: true),
                    ContactLandMark = table.Column<string>(type: "TEXT", nullable: true),
                    ContactCity = table.Column<string>(type: "TEXT", nullable: true),
                    ContactState = table.Column<string>(type: "TEXT", nullable: true),
                    ContactPinCode = table.Column<string>(type: "TEXT", nullable: true),
                    AdharNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PanNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CustmorGSTNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CustmorCountry = table.Column<string>(type: "TEXT", nullable: true),
                    CustmorZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    ContactType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "GoldStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    GrossWeight = table.Column<double>(type: "REAL", nullable: false),
                    LessWeight = table.Column<double>(type: "REAL", nullable: false),
                    NetWeight = table.Column<double>(type: "REAL", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Carrot = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequests",
                columns: table => new
                {
                    PurchaseRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatelType = table.Column<string>(type: "TEXT", nullable: true),
                    ItemType = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    ManufactureId = table.Column<string>(type: "TEXT", nullable: true),
                    ManufactureName = table.Column<string>(type: "TEXT", nullable: true),
                    GrossWeight = table.Column<double>(type: "REAL", nullable: false),
                    LessWeight = table.Column<double>(type: "REAL", nullable: false),
                    NetWeight = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<double>(type: "REAL", nullable: false),
                    Wastage = table.Column<double>(type: "REAL", nullable: false),
                    Labour = table.Column<double>(type: "REAL", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.PurchaseRequestId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "GoldStock");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
