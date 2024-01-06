using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Reposatory.Entity.Migrations
{
    /// <inheritdoc />
    public partial class updare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "comments");
        }
    }
}
