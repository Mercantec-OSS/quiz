﻿using System;
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
                    ID = table.Column<int>(type: "integer", nullable: false)
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
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quizs",
                columns: table => new
                {
                    QuizID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvitedUsers = table.Column<List<string>>(type: "text[]", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    MainDifficulty = table.Column<string>(type: "text", nullable: false),
                    UserAnswer = table.Column<List<string>>(type: "text[]", nullable: false),
                    QuizAnswer = table.Column<List<string>>(type: "text[]", nullable: false),
                    QuizDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Timer = table.Column<int>(type: "integer", nullable: false),
                    QuestionAmount = table.Column<int>(type: "integer", nullable: false),
                    CreatorID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizs", x => x.QuizID);
                    table.ForeignKey(
                        name: "FK_Quizs_Users_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedQuizs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    Results = table.Column<int>(type: "integer", nullable: false),
                    QuizID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedQuizs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompletedQuizs_Quizs_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizs",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedQuizs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    UnderCategory = table.Column<string>(type: "text", nullable: false),
                    PossibleAnswers = table.Column<int[]>(type: "integer[]", nullable: false),
                    CorrectAnswer = table.Column<int[]>(type: "integer[]", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false),
                    QuestionStatus = table.Column<bool>(type: "boolean", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    CreatorID = table.Column<int>(type: "integer", nullable: false),
                    QuizID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Quizs_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizs",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Users_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedQuizs_QuizID",
                table: "CompletedQuizs",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedQuizs_UserID",
                table: "CompletedQuizs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorID",
                table: "Questions",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizID",
                table: "Questions",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_CreatorID",
                table: "Quizs",
                column: "CreatorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedQuizs");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
