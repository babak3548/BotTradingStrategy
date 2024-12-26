using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class changeColTypeSymbbol_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossMultiple",
                table: "Symbol",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1,1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(1,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossMultiple",
                table: "Symbol",
                type: "decimal(1,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");
        }
    }
}
