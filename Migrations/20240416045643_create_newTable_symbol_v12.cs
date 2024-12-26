using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    /// <inheritdoc />
    public partial class create_newTable_symbol_v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Period = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Symbol__3214EC07A4460751", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symbol");
        }
    }
}
