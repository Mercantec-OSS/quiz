using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatorÍdId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "CreatorÍdId",
                table: "Questions",
                newName: "MainDifficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_CreatorÍdId",
                table: "Questions",
                newName: "IX_Questions_MainDifficultyId");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnderCategory",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Difficulty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GF2 = table.Column<int>(type: "integer", nullable: false),
                    H1 = table.Column<int>(type: "integer", nullable: false),
                    H2 = table.Column<int>(type: "integer", nullable: false),
                    H3 = table.Column<int>(type: "integer", nullable: false),
                    H4 = table.Column<int>(type: "integer", nullable: false),
                    H5 = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizTimer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTimer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quizs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    QuestionAmount = table.Column<int>(type: "integer", nullable: false),
                    InvitedUsers = table.Column<string>(type: "text", nullable: false),
                    UserIdId = table.Column<int>(type: "integer", nullable: false),
                    TimerId = table.Column<int>(type: "integer", nullable: false),
                    difficultyId = table.Column<int>(type: "integer", nullable: false),
                    Maindifficulty = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizs_Difficulty_difficultyId",
                        column: x => x.difficultyId,
                        principalTable: "Difficulty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizs_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizs_QuizTimer_TimerId",
                        column: x => x.TimerId,
                        principalTable: "QuizTimer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizs_Users_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_difficultyId",
                table: "Quizs",
                column: "difficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_QuestionId",
                table: "Quizs",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_TimerId",
                table: "Quizs",
                column: "TimerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_UserIdId",
                table: "Quizs",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Difficulty_MainDifficultyId",
                table: "Questions",
                column: "MainDifficultyId",
                principalTable: "Difficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatorId",
                table: "Questions",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Difficulty_MainDifficultyId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatorId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Quizs");

            migrationBuilder.DropTable(
                name: "Difficulty");

            migrationBuilder.DropTable(
                name: "QuizTimer");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UnderCategory",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "MainDifficultyId",
                table: "Questions",
                newName: "CreatorÍdId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_MainDifficultyId",
                table: "Questions",
                newName: "IX_Questions_CreatorÍdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatorÍdId",
                table: "Questions",
                column: "CreatorÍdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
