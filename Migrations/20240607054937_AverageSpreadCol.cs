using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class AverageSpreadCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Spread",
                table: "Tick",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageSpread",
                table: "Symbol",
                type: "decimal(14,6)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Spread",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "AverageSpread",
                table: "Symbol");
        }
    }
}
