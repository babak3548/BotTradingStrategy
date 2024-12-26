using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class volumeProfilerMigration_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "48_or_72LastBar_32x24bar",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "48_or_72LastBar_50x24bar",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "No_M_oPip_willTouch",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "TL_1_3M_oPip_willTouch",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "TP_M_oPip_willTouch",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "decrease_N_opip",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "dontChange",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "increace_N_opip",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "lastBar32x24bar",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "lastBar50x24bar",
                table: "PreProcessData");

            migrationBuilder.AlterColumn<decimal>(
                name: "open",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "low",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "high",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "close",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VolumeProfiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    lastBar32x24bar = table.Column<int>(type: "int", nullable: true),
                    lastBar50x24bar = table.Column<int>(type: "int", nullable: true),
                    _48Or72lastBar32x24bar = table.Column<int>(type: "int", nullable: true),
                    _48Or72lastBar50x24bar = table.Column<int>(type: "int", nullable: true),
                    IncreaceNOpip = table.Column<bool>(type: "bit", nullable: true),
                    DecreaseNOpip = table.Column<bool>(type: "bit", nullable: true),
                    DontChange = table.Column<bool>(type: "bit", nullable: true),
                    TpMOPipWillTouch = table.Column<bool>(type: "bit", nullable: true),
                    Tl13mOPipWillTouch = table.Column<bool>(type: "bit", nullable: true),
                    NoMOPipWillTouch = table.Column<bool>(type: "bit", nullable: true),
                    PreProcessDatumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VolumeProfiler__3214EC07A4460751", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeProfiler_PreProcessData_PreProcessDatumId",
                        column: x => x.PreProcessDatumId,
                        principalTable: "PreProcessData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolumeProfiler_PreProcessDatumId",
                table: "VolumeProfiler",
                column: "PreProcessDatumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolumeProfiler");

            migrationBuilder.AlterColumn<decimal>(
                name: "open",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "low",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "high",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "close",
                table: "PreProcessData",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<int>(
                name: "48_or_72LastBar_32x24bar",
                table: "PreProcessData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "48_or_72LastBar_50x24bar",
                table: "PreProcessData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "No_M_oPip_willTouch",
                table: "PreProcessData",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TL_1_3M_oPip_willTouch",
                table: "PreProcessData",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TP_M_oPip_willTouch",
                table: "PreProcessData",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "decrease_N_opip",
                table: "PreProcessData",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "dontChange",
                table: "PreProcessData",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "increace_N_opip",
                table: "PreProcessData",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "lastBar32x24bar",
                table: "PreProcessData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "lastBar50x24bar",
                table: "PreProcessData",
                type: "int",
                nullable: true);
        }
    }
}
