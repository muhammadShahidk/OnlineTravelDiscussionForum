using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class ReplyiesAddedToDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_AspNetUsers_UserID",
                table: "Reply");

            migrationBuilder.DropForeignKey(
                name: "FK_Reply_Comments_CommentId",
                table: "Reply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reply",
                table: "Reply");

            migrationBuilder.RenameTable(
                name: "Reply",
                newName: "Replies");

            migrationBuilder.RenameIndex(
                name: "IX_Reply_UserID",
                table: "Replies",
                newName: "IX_Replies_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reply_CommentId",
                table: "Replies",
                newName: "IX_Replies_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Replies",
                table: "Replies",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_UserID",
                table: "Replies",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_CommentId",
                table: "Replies",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_UserID",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_CommentId",
                table: "Replies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Replies",
                table: "Replies");

            migrationBuilder.RenameTable(
                name: "Replies",
                newName: "Reply");

            migrationBuilder.RenameIndex(
                name: "IX_Replies_UserID",
                table: "Reply",
                newName: "IX_Reply_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Replies_CommentId",
                table: "Reply",
                newName: "IX_Reply_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reply",
                table: "Reply",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_AspNetUsers_UserID",
                table: "Reply",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_Comments_CommentId",
                table: "Reply",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
