using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class renameTableCandle_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeProfiler_PreProcessData_PreProcessDatumId",
                table: "VolumeProfiler");

            migrationBuilder.RenameTable(
                name: "PreProcessData",
                newName: "Candle");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeProfiler_Candle_PreProcessDatumId",
                table: "VolumeProfiler",
                column: "PreProcessDatumId",
                principalTable: "Candle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeProfiler_Candle_PreProcessDatumId",
                table: "VolumeProfiler");

            migrationBuilder.RenameTable(
                name: "Candle",
                newName: "PreProcessData");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeProfiler_PreProcessData_PreProcessDatumId",
                table: "VolumeProfiler",
                column: "PreProcessDatumId",
                principalTable: "PreProcessData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
