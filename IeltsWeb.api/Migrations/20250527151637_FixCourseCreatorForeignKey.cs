using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IeltsWeb.api.Migrations
{
    /// <inheritdoc />
    public partial class FixCourseCreatorForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_CreatorUserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CreatorUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_CreatedBy",
                table: "Courses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_CreatedBy",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatorUserId",
                table: "Courses",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_CreatorUserId",
                table: "Courses",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
