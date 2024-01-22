using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class banuserKeyupdatedToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BandUsers",
                table: "BandUsers");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "BandUsers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BandUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandUsers",
                table: "BandUsers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BandUsers",
                table: "BandUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BandUsers");

            migrationBuilder.AddColumn<string>(
                name: "BandId",
                table: "BandUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandUsers",
                table: "BandUsers",
                column: "BandId");
        }
    }
}
