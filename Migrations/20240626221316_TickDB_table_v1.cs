using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class TickDB_table_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TickDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TickDBDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandleDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ask = table.Column<decimal>(type: "decimal(12,6)", nullable: false),
                    Bid = table.Column<decimal>(type: "decimal(12,6)", nullable: false),
                    Period = table.Column<decimal>(type: "decimal(12,6)", nullable: false),
                    SymbolId = table.Column<int>(type: "int", nullable: false),
                    Tradeable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TickDB__3214EC07A4460751", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickDB");
        }
    }
}
