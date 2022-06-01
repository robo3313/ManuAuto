using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutoIdentity.Data.Migrations
{
    public partial class RemoveTutorialAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tutorials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Tutorials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
