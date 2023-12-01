using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class relationsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Users_UserID",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserID",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "ApprovalRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "ApprovalRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID1",
                table: "Posts",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID1",
                table: "Comments",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_UserID1",
                table: "ApprovalRequests",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_AspNetUsers_UserID",
                table: "ApprovalRequests",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Users_UserID1",
                table: "ApprovalRequests",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID1",
                table: "Comments",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserID1",
                table: "Posts",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_AspNetUsers_UserID",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Users_UserID1",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserID1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserID1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserID1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_UserID1",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Users_UserID",
                table: "ApprovalRequests",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
