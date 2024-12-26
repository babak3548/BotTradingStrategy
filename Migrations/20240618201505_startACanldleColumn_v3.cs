using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class startACanldleColumn_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start_a_Candle",
                table: "Symbol",
                newName: "StartCandle");

            migrationBuilder.RenameColumn(
                name: "EfectedVolumeOnNextCandles",
                table: "Symbol",
                newName: "RecentCandlesForVolumeSelector");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartCandle",
                table: "Symbol",
                newName: "Start_a_Candle");

            migrationBuilder.RenameColumn(
                name: "RecentCandlesForVolumeSelector",
                table: "Symbol",
                newName: "EfectedVolumeOnNextCandles");
        }
    }
}
