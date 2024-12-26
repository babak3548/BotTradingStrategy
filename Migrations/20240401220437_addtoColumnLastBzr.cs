using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class addtoColumnLastBzr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastBar32x24bar",
                table: "PreProcessData");

            migrationBuilder.DropColumn(
                name: "lastBar50x24bar",
                table: "PreProcessData");
        }
    }
}
