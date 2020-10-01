using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogNetCore.DataAccess.Migrations
{
    public partial class UpdateArticleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Article",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Article");
        }
    }
}
