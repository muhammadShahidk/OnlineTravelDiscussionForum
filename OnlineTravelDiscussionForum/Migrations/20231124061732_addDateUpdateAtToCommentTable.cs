using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTravelDiscussionForum.Migrations
{
    /// <inheritdoc />
    public partial class addDateUpdateAtToCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCommented",
                table: "Comments",
                newName: "DateCreateAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdateAt",
                table: "Comments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUpdateAt",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "DateCreateAt",
                table: "Comments",
                newName: "DateCommented");
        }
    }
}
