using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class permissiontradetSymboleCol_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermisionTradeType",
                table: "Symbol",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermisionTradeType",
                table: "Symbol");
        }
    }
}
