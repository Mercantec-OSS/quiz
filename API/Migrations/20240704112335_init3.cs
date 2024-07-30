using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizs_QuizTimer_TimerId",
                table: "Quizs");

            migrationBuilder.DropTable(
                name: "QuizTimer");

            migrationBuilder.DropIndex(
                name: "IX_Quizs_TimerId",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "TimerId",
                table: "Quizs");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timer",
                table: "Quizs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timer",
                table: "Quizs");

            migrationBuilder.AddColumn<int>(
                name: "TimerId",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuizTimer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTimer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_TimerId",
                table: "Quizs",
                column: "TimerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizs_QuizTimer_TimerId",
                table: "Quizs",
                column: "TimerId",
                principalTable: "QuizTimer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
