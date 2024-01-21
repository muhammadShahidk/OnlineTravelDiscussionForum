using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class banUserRelationToUserUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandUsers_AspNetUsers_UserId",
                table: "BandUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BandUsers",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_BandUsers_UserId",
                table: "BandUsers",
                newName: "IX_BandUsers_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BandUsers_AspNetUsers_UserID",
                table: "BandUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandUsers_AspNetUsers_UserID",
                table: "BandUsers");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "BandUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BandUsers_UserID",
                table: "BandUsers",
                newName: "IX_BandUsers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BandUsers_AspNetUsers_UserId",
                table: "BandUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
