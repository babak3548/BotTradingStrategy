using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class chageTypesTables_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Low",
                table: "VolumeProfiler",
                type: "decimal(12,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "High",
                table: "VolumeProfiler",
                type: "decimal(12,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VpLowHigh",
                table: "Symbol",
                type: "decimal(12,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossMultiple",
                table: "Symbol",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfirmTrendNum",
                table: "Symbol",
                type: "decimal(12,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VPlow_high",
                table: "Order",
                type: "decimal(14,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfit",
                table: "Order",
                type: "decimal(14,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLoss",
                table: "Order",
                type: "decimal(14,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Revenue",
                table: "Order",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpenTickPrice",
                table: "Order",
                type: "decimal(14,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LotSize",
                table: "Order",
                type: "decimal(12,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CloseTickPrice",
                table: "Order",
                type: "decimal(14,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Low",
                table: "VolumeProfiler",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "High",
                table: "VolumeProfiler",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VpLowHigh",
                table: "Symbol",
                type: "decimal(6,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfitMultiple",
                table: "Symbol",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossMultiple",
                table: "Symbol",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfirmTrendNum",
                table: "Symbol",
                type: "decimal(6,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VPlow_high",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TakeProfit",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StopLoss",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Revenue",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpenTickPrice",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LotSize",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CloseTickPrice",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)");
        }
    }
}
