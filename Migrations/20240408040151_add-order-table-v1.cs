using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class addordertablev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolumeProfilerId = table.Column<int>(type: "int", nullable: false),
                    TickPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TickDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastBarsRepetion = table.Column<int>(type: "int", nullable: true),
                    CounterCandelDiff = table.Column<int>(type: "int", nullable: false),
                    OrdreType = table.Column<int>(type: "int", nullable: false),
                    TakeProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StopLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VPlow_high = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LotSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReasonCloseOrders = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__3214EC07A4460751", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_VolumeProfiler_VolumeProfilerId",
                        column: x => x.VolumeProfilerId,
                        principalTable: "VolumeProfiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_VolumeProfilerId",
                table: "Order",
                column: "VolumeProfilerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
