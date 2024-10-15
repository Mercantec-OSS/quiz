﻿using System;
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
                name: "Difficulties",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Difficulty = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Education = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Education = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categories_Educations_Education",
                        column: x => x.Education,
                        principalTable: "Educations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    RolesID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RolesID",
                        column: x => x.RolesID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnderCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnderCategory = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnderCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnderCategories_Categories_Category",
                        column: x => x.Category,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    EducationID = table.Column<int>(type: "integer", nullable: false),
                    EducationsID = table.Column<int>(type: "integer", nullable: false),
                    CategoryID = table.Column<int>(type: "integer", nullable: false),
                    CategoriesID = table.Column<int>(type: "integer", nullable: false),
                    DifficultyID = table.Column<int>(type: "integer", nullable: false),
                    DifficultiesID = table.Column<int>(type: "integer", nullable: false),
                    Timer = table.Column<int>(type: "integer", nullable: false),
                    QuestionAmount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Quizs_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizs_Difficulties_DifficultiesID",
                        column: x => x.DifficultiesID,
                        principalTable: "Difficulties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizs_Educations_EducationsID",
                        column: x => x.EducationsID,
                        principalTable: "Educations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    UnderCategory = table.Column<int>(type: "integer", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    PossibleAnswers = table.Column<string[]>(type: "text[]", nullable: false),
                    CorrectAnswer = table.Column<int[]>(type: "integer[]", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false),
                    QuestionStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_Categories_Category",
                        column: x => x.Category,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Difficulties_Difficulty",
                        column: x => x.Difficulty,
                        principalTable: "Difficulties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_UnderCategories_UnderCategory",
                        column: x => x.UnderCategory,
                        principalTable: "UnderCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Quiz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    Results = table.Column<int>(type: "integer", nullable: false),
                    QuizEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Quiz", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Quiz_Quizs_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Quiz_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quiz_Question",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quiz = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz_Question", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Quiz_Question_Questions_Question",
                        column: x => x.Question,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quiz_Question_Quizs_Quiz",
                        column: x => x.Quiz,
                        principalTable: "Quizs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Education",
                table: "Categories",
                column: "Education");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Category",
                table: "Questions",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Difficulty",
                table: "Questions",
                column: "Difficulty");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UnderCategory",
                table: "Questions",
                column: "UnderCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserID",
                table: "Questions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_Question_Question",
                table: "Quiz_Question",
                column: "Question");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_Question_Quiz",
                table: "Quiz_Question",
                column: "Quiz");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_CategoriesID",
                table: "Quizs",
                column: "CategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_DifficultiesID",
                table: "Quizs",
                column: "DifficultiesID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_EducationsID",
                table: "Quizs",
                column: "EducationsID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_UserID",
                table: "Quizs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnderCategories_Category",
                table: "UnderCategories",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_User_Quiz_QuizID",
                table: "User_Quiz",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Quiz_UserID",
                table: "User_Quiz",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolesID",
                table: "Users",
                column: "RolesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quiz_Question");

            migrationBuilder.DropTable(
                name: "User_Quiz");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizs");

            migrationBuilder.DropTable(
                name: "UnderCategories");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Educations");
        }
    }
}