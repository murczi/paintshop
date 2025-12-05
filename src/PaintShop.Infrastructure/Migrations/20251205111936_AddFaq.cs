using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaintShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFaq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsInside",
                table: "Product",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "Paid",
                table: "Orders",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(1)");

            migrationBuilder.CreateTable(
                name: "Faq",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Answer = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faq", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faq");

            migrationBuilder.AlterColumn<sbyte>(
                name: "IsInside",
                table: "Product",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit(1)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "Paid",
                table: "Orders",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit(1)");
        }
    }
}
