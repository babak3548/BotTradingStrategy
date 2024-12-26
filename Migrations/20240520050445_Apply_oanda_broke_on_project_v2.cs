using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class Apply_oanda_broke_on_project_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Tradeable",
                table: "Tick",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosAccountBalance",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosCloseoutAsk",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosCloseoutBid",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosCommission",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosFinancing",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExClosePosFullResponseBody",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosFullVWAP",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosGainQuoteHomeConversionFactor",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosHalfSpreadCost",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosLossQuoteHomeConversionFactor",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ExClosePosMarketStatus",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosPL",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosPriceTran",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExClosePosReason",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExClosePosRelatedTransactionIDs",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosRequestedUnits",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExClosePosSymbol",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExClosePosTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExClosePosTradeIds",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExClosePosUnits",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespAccountBalance",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespCloseoutAsk",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespCloseoutBid",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespCommission",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespFinancing",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExCreateRespFullResponseBody",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespGainQuoteHomeConversionFactor",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespHalfSpreadCost",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespInitialMarginRequired",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespLossQuoteHomeConversionFactor",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ExCreateRespMarketStatus",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespPL",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespPriceTran",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExCreateRespReason",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExCreateRespRelatedTransactionIDs",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExCreateRespTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExCreateRespTradeIds",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExCreateRespUnits",
                table: "Order",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tradeable",
                table: "Tick");

            migrationBuilder.DropColumn(
                name: "ExClosePosAccountBalance",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosCloseoutAsk",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosCloseoutBid",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosCommission",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosFinancing",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosFullResponseBody",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosFullVWAP",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosGainQuoteHomeConversionFactor",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosHalfSpreadCost",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosLossQuoteHomeConversionFactor",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosMarketStatus",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosPL",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosPriceTran",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosReason",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosRelatedTransactionIDs",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosRequestedUnits",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosSymbol",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosTime",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosTradeIds",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExClosePosUnits",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespAccountBalance",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespCloseoutAsk",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespCloseoutBid",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespCommission",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespFinancing",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespFullResponseBody",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespGainQuoteHomeConversionFactor",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespHalfSpreadCost",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespInitialMarginRequired",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespLossQuoteHomeConversionFactor",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespMarketStatus",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespPL",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespPriceTran",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespReason",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespRelatedTransactionIDs",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespTime",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespTradeIds",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExCreateRespUnits",
                table: "Order");
        }
    }
}
