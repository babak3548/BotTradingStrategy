using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class TradeAgentRefactor_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastBar32x24bar",
                table: "VolumeProfiler",
                newName: "LastBar32x24Bar");

            migrationBuilder.RenameColumn(
                name: "VPlowHigh",
                table: "Symbol",
                newName: "VpLowHigh");

            migrationBuilder.RenameColumn(
                name: "StoplossMultiple",
                table: "Symbol",
                newName: "StopLossMultiple");

            migrationBuilder.RenameColumn(
                name: "ConfimTrandNum",
                table: "Symbol",
                newName: "ConfirmTrendNum");

            migrationBuilder.RenameColumn(
                name: "OrdreType",
                table: "Order",
                newName: "OrderType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastBar32x24Bar",
                table: "VolumeProfiler",
                newName: "lastBar32x24bar");

            migrationBuilder.RenameColumn(
                name: "VpLowHigh",
                table: "Symbol",
                newName: "VPlowHigh");

            migrationBuilder.RenameColumn(
                name: "StopLossMultiple",
                table: "Symbol",
                newName: "StoplossMultiple");

            migrationBuilder.RenameColumn(
                name: "ConfirmTrendNum",
                table: "Symbol",
                newName: "ConfimTrandNum");

            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Order",
                newName: "OrdreType");
        }
    }
}
