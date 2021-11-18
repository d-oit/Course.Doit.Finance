using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class AddInvestementandtransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinanceAccountId = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ISIN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WKN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InvestmentEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmmissionNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TACode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InvestmentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NominalValueOfAnInvestment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    DividendReturnPercentage = table.Column<double>(type: "float", nullable: true),
                    TotalReturnPercentage = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    InterestClaimSinceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysLate = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investment_FinanceAccounts_FinanceAccountId",
                        column: x => x.FinanceAccountId,
                        principalTable: "FinanceAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    FinanceAccountId = table.Column<int>(type: "int", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    IsInitAmount = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Favorit = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsedFormula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    BookingType = table.Column<int>(type: "int", nullable: false),
                    CounterAccountId = table.Column<int>(type: "int", nullable: true),
                    InvestmentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_FinanceAccounts_CounterAccountId",
                        column: x => x.CounterAccountId,
                        principalTable: "FinanceAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_FinanceAccounts_FinanceAccountId",
                        column: x => x.FinanceAccountId,
                        principalTable: "FinanceAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Investment_InvestmentId",
                        column: x => x.InvestmentId,
                        principalTable: "Investment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccounts_Name",
                table: "FinanceAccounts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investment_FinanceAccountId",
                table: "Investment",
                column: "FinanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Investment_No_FinanceAccountId",
                table: "Investment",
                columns: new[] { "No", "FinanceAccountId" },
                unique: true,
                filter: "[No] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CounterAccountId",
                table: "Transactions",
                column: "CounterAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinanceAccountId",
                table: "Transactions",
                column: "FinanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvestmentId",
                table: "Transactions",
                column: "InvestmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropIndex(
                name: "IX_FinanceAccounts_Name",
                table: "FinanceAccounts");
        }
    }
}
