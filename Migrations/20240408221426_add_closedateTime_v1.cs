using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class add_closedateTime_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TickDateTime",
                table: "Order",
                newName: "OpenTickDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTickDatetime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTickDatetime",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "OpenTickDateTime",
                table: "Order",
                newName: "TickDateTime");
        }
    }
}
