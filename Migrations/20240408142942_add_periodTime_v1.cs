using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class add_periodTime_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PeriodTickMin",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodTickMin",
                table: "Order");
        }
    }
}
