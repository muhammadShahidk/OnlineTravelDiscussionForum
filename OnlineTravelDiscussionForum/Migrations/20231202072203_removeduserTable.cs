using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class removeduserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Users_UserID1",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserID1",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "ApprovalRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

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
                name: "FK_ApprovalRequests_Users_UserID1",
                table: "ApprovalRequests",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID1",
                table: "Comments",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserID1",
                table: "Posts",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
