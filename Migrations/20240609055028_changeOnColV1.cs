using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class changeOnColV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Spread",
                table: "Tick",
                newName: "Bid");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Tick",
                newName: "Ask");

            migrationBuilder.AddColumn<int>(
                name: "AskLiquiditySum",
                table: "Tick",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BidLiquiditySum",
                table: "Tick",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AskLiquiditySum",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "BidLiquiditySum",
                table: "Tick");

            migrationBuilder.RenameColumn(
                name: "Bid",
                table: "Tick",
                newName: "Spread");

            migrationBuilder.RenameColumn(
                name: "Ask",
                table: "Tick",
                newName: "Price");
        }
    }
}
