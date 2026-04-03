using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningCoachAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalToSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Subjects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LearningSessions_SubjectId",
                table: "LearningSessions",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningSessions_Subjects_SubjectId",
                table: "LearningSessions",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningSessions_Subjects_SubjectId",
                table: "LearningSessions");

            migrationBuilder.DropIndex(
                name: "IX_LearningSessions_SubjectId",
                table: "LearningSessions");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Subjects");
        }
    }
}
