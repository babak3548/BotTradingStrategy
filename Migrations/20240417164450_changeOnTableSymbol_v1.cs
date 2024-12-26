using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class changeOnTableSymbol_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tick_Candle_CandleId",
                table: "Tick");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Tick__3214EC07A4460751",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "Close",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "Datetime",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "High",
                table: "Tick");

            migrationBuilder.RenameColumn(
                name: "Open",
                table: "Tick",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Low",
                table: "Tick",
                newName: "Period");

            migrationBuilder.AlterColumn<int>(
                name: "CandleId",
                table: "Tick",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CandleDatetime",
                table: "Tick",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SymbolId",
                table: "Tick",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TickDatetime",
                table: "Tick",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCandleUpdated",
                table: "Symbol",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tick",
                table: "Tick",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tick_Candle_CandleId",
                table: "Tick",
                column: "CandleId",
                principalTable: "Candle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tick_Candle_CandleId",
                table: "Tick");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tick",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "CandleDatetime",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "SymbolId",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "TickDatetime",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "LastCandleUpdated",
                table: "Symbol");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Tick",
                newName: "Open");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "Tick",
                newName: "Low");

            migrationBuilder.AlterColumn<int>(
                name: "CandleId",
                table: "Tick",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Close",
                table: "Tick",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Datetime",
                table: "Tick",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "High",
                table: "Tick",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Tick__3214EC07A4460751",
                table: "Tick",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tick_Candle_CandleId",
                table: "Tick",
                column: "CandleId",
                principalTable: "Candle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
