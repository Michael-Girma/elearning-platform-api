using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedResourcesAndAssessments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentQuestions_QuestionChoices_AnswerQuestionChoiceId",
                table: "AssessmentQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentQuestions_AnswerQuestionChoiceId",
                table: "AssessmentQuestions");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "AssessmentQuestions",
                newName: "AnswerQuestionChoiceQuestionChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestions_AnswerQuestionChoiceQuestionChoiceId",
                table: "AssessmentQuestions",
                column: "AnswerQuestionChoiceQuestionChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentQuestions_QuestionChoices_AnswerQuestionChoiceQue~",
                table: "AssessmentQuestions",
                column: "AnswerQuestionChoiceQuestionChoiceId",
                principalTable: "QuestionChoices",
                principalColumn: "QuestionChoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentQuestions_QuestionChoices_AnswerQuestionChoiceQue~",
                table: "AssessmentQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentQuestions_AnswerQuestionChoiceQuestionChoiceId",
                table: "AssessmentQuestions");

            migrationBuilder.RenameColumn(
                name: "AnswerQuestionChoiceQuestionChoiceId",
                table: "AssessmentQuestions",
                newName: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestions_AnswerQuestionChoiceId",
                table: "AssessmentQuestions",
                column: "AnswerQuestionChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentQuestions_QuestionChoices_AnswerQuestionChoiceId",
                table: "AssessmentQuestions",
                column: "AnswerQuestionChoiceId",
                principalTable: "QuestionChoices",
                principalColumn: "QuestionChoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
