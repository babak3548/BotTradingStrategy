using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class renameSymboleCol_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinLastBarVolProfile",
                table: "Symbol",
                newName: "MinRepetationVolumeNeededTrade");

            migrationBuilder.RenameColumn(
                name: "BackRangeCandles",
                table: "Symbol",
                newName: "EfectedVolumeOnNextCandles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinRepetationVolumeNeededTrade",
                table: "Symbol",
                newName: "MinLastBarVolProfile");

            migrationBuilder.RenameColumn(
                name: "EfectedVolumeOnNextCandles",
                table: "Symbol",
                newName: "BackRangeCandles");
        }
    }
}
