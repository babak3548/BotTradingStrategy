using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTick_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeProfiler_Candle_PreProcessDatumId",
                table: "VolumeProfiler");

            migrationBuilder.RenameColumn(
                name: "PreProcessDatumId",
                table: "VolumeProfiler",
                newName: "CandlesId");

            migrationBuilder.RenameIndex(
                name: "IX_VolumeProfiler_PreProcessDatumId",
                table: "VolumeProfiler",
                newName: "IX_VolumeProfiler_CandlesId");

            migrationBuilder.CreateTable(
                name: "Tick",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CandleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tick__3214EC07A4460751", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tick_Candle_CandleId",
                        column: x => x.CandleId,
                        principalTable: "Candle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tick_CandleId",
                table: "Tick",
                column: "CandleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeProfiler_Candle_CandlesId",
                table: "VolumeProfiler",
                column: "CandlesId",
                principalTable: "Candle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeProfiler_Candle_CandlesId",
                table: "VolumeProfiler");

            migrationBuilder.DropTable(
                name: "Tick");

            migrationBuilder.RenameColumn(
                name: "CandlesId",
                table: "VolumeProfiler",
                newName: "PreProcessDatumId");

            migrationBuilder.RenameIndex(
                name: "IX_VolumeProfiler_CandlesId",
                table: "VolumeProfiler",
                newName: "IX_VolumeProfiler_PreProcessDatumId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeProfiler_Candle_PreProcessDatumId",
                table: "VolumeProfiler",
                column: "PreProcessDatumId",
                principalTable: "Candle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
