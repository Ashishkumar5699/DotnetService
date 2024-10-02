using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sonaar.Service.APi.Migrations
{
    /// <inheritdoc />
    public partial class Quotationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GstAmountEntity",
                columns: table => new
                {
                    GstAmountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CGSt = table.Column<decimal>(type: "TEXT", nullable: false),
                    SGST = table.Column<decimal>(type: "TEXT", nullable: false),
                    IGST = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalBeforeDiscount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GstAmountEntity", x => x.GstAmountId);
                });

            migrationBuilder.CreateTable(
                name: "Quotations",
                columns: table => new
                {
                    QuotationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Billid = table.Column<int>(type: "INTEGER", nullable: false),
                    BillType = table.Column<int>(type: "INTEGER", nullable: false),
                    DateofBill = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: true),
                    GstAmountId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotations", x => x.QuotationId);
                    table.ForeignKey(
                        name: "FK_Quotations_ContactDetails_ContactId",
                        column: x => x.ContactId,
                        principalTable: "ContactDetails",
                        principalColumn: "ContactId");
                    table.ForeignKey(
                        name: "FK_Quotations_GstAmountEntity_GstAmountId",
                        column: x => x.GstAmountId,
                        principalTable: "GstAmountEntity",
                        principalColumn: "GstAmountId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuotationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    HSN_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Purity = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Making_Charge = table.Column<decimal>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "QuotationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_QuotationId",
                table: "Products",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_ContactId",
                table: "Quotations",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_GstAmountId",
                table: "Quotations",
                column: "GstAmountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Quotations");

            migrationBuilder.DropTable(
                name: "GstAmountEntity");
        }
    }
}
