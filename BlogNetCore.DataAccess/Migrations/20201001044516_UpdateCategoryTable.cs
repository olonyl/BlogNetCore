using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogNetCore.DataAccess.Migrations
{
    public partial class UpdateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Category",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sort",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
