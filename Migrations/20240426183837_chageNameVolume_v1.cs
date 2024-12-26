using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class chageNameVolume_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecreaseNOpip",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "DontChange",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "IncreaceNOpip",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "LastBar32x24Bar",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "NoMOPipWillTouch",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "Tl13mOPipWillTouch",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "TpMOPipWillTouch",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "_48Or72lastBar32x24bar",
                table: "VolumeProfiler");

            migrationBuilder.DropColumn(
                name: "_48Or72lastBar50x24bar",
                table: "VolumeProfiler");

            migrationBuilder.RenameColumn(
                name: "lastBar50x24bar",
                table: "VolumeProfiler",
                newName: "LastBarRepetationVolume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastBarRepetationVolume",
                table: "VolumeProfiler",
                newName: "lastBar50x24bar");

            migrationBuilder.AddColumn<bool>(
                name: "DecreaseNOpip",
                table: "VolumeProfiler",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DontChange",
                table: "VolumeProfiler",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncreaceNOpip",
                table: "VolumeProfiler",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastBar32x24Bar",
                table: "VolumeProfiler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoMOPipWillTouch",
                table: "VolumeProfiler",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tl13mOPipWillTouch",
                table: "VolumeProfiler",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TpMOPipWillTouch",
                table: "VolumeProfiler",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "_48Or72lastBar32x24bar",
                table: "VolumeProfiler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "_48Or72lastBar50x24bar",
                table: "VolumeProfiler",
                type: "int",
                nullable: true);
        }
    }
}
