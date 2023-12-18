using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginRegisterIdentity.Migrations
{
    /// <inheritdoc />
    public partial class photoLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureLink",
                table: "AspNetUsers");
        }
    }
}
