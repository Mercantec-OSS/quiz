using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Role = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Quizs",
                columns: table => new
                {
                    QuizID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    InvitedUsers = table.Column<List<string>>(type: "text[]", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    UserAnswer = table.Column<int[]>(type: "integer[]", nullable: false),
                    QuizDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Timer = table.Column<int>(type: "integer", nullable: false),
                    MainDifficulty = table.Column<string>(type: "text", nullable: false),
                    CreatorID = table.Column<int>(type: "integer", nullable: false),
                    QuestionAmount = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizs", x => x.QuizID);
                    table.ForeignKey(
                        name: "FK_Quizs_Users_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quizs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Difficulty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionID = table.Column<int>(type: "integer", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizID = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    UnderCategory = table.Column<string>(type: "text", nullable: false),
                    PossibleAnswers = table.Column<int[]>(type: "integer[]", nullable: false),
                    CorrectAnswer = table.Column<int[]>(type: "integer[]", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false),
                    QuestionStatus = table.Column<bool>(type: "boolean", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "text", nullable: false),
                    MainDifficultyId = table.Column<int>(type: "integer", nullable: false),
                    CreatorID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Difficulty_MainDifficultyId",
                        column: x => x.MainDifficultyId,
                        principalTable: "Difficulty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_Quizs_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizs",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Difficulty_QuestionID",
                table: "Difficulty",
                column: "QuestionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MainDifficultyId",
                table: "Questions",
                column: "MainDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizID",
                table: "Questions",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_CreatorID",
                table: "Quizs",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_UserID",
                table: "Quizs",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Difficulty_Questions_QuestionID",
                table: "Difficulty",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Difficulty_Questions_QuestionID",
                table: "Difficulty");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Difficulty");

            migrationBuilder.DropTable(
                name: "Quizs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
