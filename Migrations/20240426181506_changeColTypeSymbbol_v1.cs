using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class changeColTypeSymbbol_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VpLowHigh",
                table: "Symbol",
                type: "decimal(6,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(1,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossMultiple",
                table: "Symbol",
                type: "decimal(1,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<byte>(
                name: "PercentageInvestTrade",
                table: "Symbol",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfirmTrendNum",
                table: "Symbol",
                type: "decimal(6,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VpLowHigh",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossMultiple",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageInvestTrade",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfirmTrendNum",
                table: "Symbol",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)");
        }
    }
}
