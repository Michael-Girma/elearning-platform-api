using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class updatedQuestionChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentQuestions_QuestionChoices_AnswerQuestionChoiceQue~",
                table: "AssessmentQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentQuestions_AnswerQuestionChoiceQuestionChoiceId",
                table: "AssessmentQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerQuestionChoiceId",
                table: "AssessmentQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerQuestionChoiceQuestionChoiceId",
                table: "AssessmentQuestions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectAnswer",
                table: "QuestionChoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrectAnswer",
                table: "QuestionChoices");

            migrationBuilder.AddColumn<Guid>(
                name: "AnswerQuestionChoiceId",
                table: "AssessmentQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AnswerQuestionChoiceQuestionChoiceId",
                table: "AssessmentQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
