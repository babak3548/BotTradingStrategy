using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class cleare_candels_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeProfiler_Candle_CandlesId",
                table: "VolumeProfiler");

            migrationBuilder.RenameColumn(
                name: "CandlesId",
                table: "VolumeProfiler",
                newName: "CandleId");

            migrationBuilder.RenameIndex(
                name: "IX_VolumeProfiler_CandlesId",
                table: "VolumeProfiler",
                newName: "IX_VolumeProfiler_CandleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeProfiler_Candle_CandleId",
                table: "VolumeProfiler",
                column: "CandleId",
                principalTable: "Candle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeProfiler_Candle_CandleId",
                table: "VolumeProfiler");

            migrationBuilder.RenameColumn(
                name: "CandleId",
                table: "VolumeProfiler",
                newName: "CandlesId");

            migrationBuilder.RenameIndex(
                name: "IX_VolumeProfiler_CandleId",
                table: "VolumeProfiler",
                newName: "IX_VolumeProfiler_CandlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeProfiler_Candle_CandlesId",
                table: "VolumeProfiler",
                column: "CandlesId",
                principalTable: "Candle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
