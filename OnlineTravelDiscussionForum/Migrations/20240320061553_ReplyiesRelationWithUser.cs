using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class ReplyiesRelationWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_AspNetUsers_UserID",
                table: "Reply");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_AspNetUsers_UserID",
                table: "Reply",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_AspNetUsers_UserID",
                table: "Reply");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_AspNetUsers_UserID",
                table: "Reply",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
