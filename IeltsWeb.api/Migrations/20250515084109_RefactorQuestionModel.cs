using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IeltsWeb.api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamResultAnswerOptions");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.AddColumn<string>(
                name: "OptionA",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OptionB",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OptionC",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OptionD",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "ExamResults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_QuestionId",
                table: "ExamResults",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Questions_QuestionId",
                table: "ExamResults",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Questions_QuestionId",
                table: "ExamResults");

            migrationBuilder.DropIndex(
                name: "IX_ExamResults_QuestionId",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "OptionA",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "OptionB",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "OptionC",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "OptionD",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "ExamResults");

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionKey = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExamResultAnswerOptions",
                columns: table => new
                {
                    ExamResultsResultId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResultAnswerOptions", x => new { x.ExamResultsResultId, x.SelectedOptionsId });
                    table.ForeignKey(
                        name: "FK_ExamResultAnswerOptions_AnswerOptions_SelectedOptionsId",
                        column: x => x.SelectedOptionsId,
                        principalTable: "AnswerOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResultAnswerOptions_ExamResults_ExamResultsResultId",
                        column: x => x.ExamResultsResultId,
                        principalTable: "ExamResults",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_QuestionId",
                table: "AnswerOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResultAnswerOptions_SelectedOptionsId",
                table: "ExamResultAnswerOptions",
                column: "SelectedOptionsId");
        }
    }
}
