using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBR.Storage.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EngName = table.Column<string>(type: "text", nullable: false),
                    ParentCode = table.Column<string>(type: "text", nullable: false),
                    Nominal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyCourses",
                columns: table => new
                {
                    CurrencyId = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumCode = table.Column<string>(type: "text", nullable: false),
                    CharCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Nominal = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    VunitRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyCourses", x => new { x.CurrencyId, x.Date });
                    table.ForeignKey(
                        name: "FK_CurrencyCourses_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyCourses_CurrencyId_Date",
                table: "CurrencyCourses",
                columns: new[] { "CurrencyId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyCourses");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
