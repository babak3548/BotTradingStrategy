using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class AddAutoIncrementToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreProcessData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    open = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    high = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    low = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    close = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    _48_or_72LastBar_32x24bar = table.Column<int>(name: "48_or_72LastBar_32x24bar", type: "int", nullable: true),
                    _48_or_72LastBar_50x24bar = table.Column<int>(name: "48_or_72LastBar_50x24bar", type: "int", nullable: true),
                    increace_N_opip = table.Column<bool>(type: "bit", nullable: true),
                    decrease_N_opip = table.Column<bool>(type: "bit", nullable: true),
                    dontChange = table.Column<bool>(type: "bit", nullable: true),
                    TP_M_oPip_willTouch = table.Column<bool>(type: "bit", nullable: true),
                    TL_1_3M_oPip_willTouch = table.Column<bool>(type: "bit", nullable: true),
                    No_M_oPip_willTouch = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PreProce__3214EC07CE0CEDF9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table__3214EC07A4460751", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table1__3214EC07CCC77CF8", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreProcessData");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "Table1");
        }
    }
}
