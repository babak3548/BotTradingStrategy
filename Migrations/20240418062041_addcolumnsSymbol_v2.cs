using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnsSymbol_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BackRangeCandles",
                table: "Symbol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ConfimTrandNum",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MinLastBarVolProfile",
                table: "Symbol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageInvestTrade",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StoplossMultiple",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VPlowHigh",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackRangeCandles",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "ConfimTrandNum",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "MinLastBarVolProfile",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "PercentageInvestTrade",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "StoplossMultiple",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "TakeProfitMultiple",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "VPlowHigh",
                table: "Symbol");
        }
    }
}
