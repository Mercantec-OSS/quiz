using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionTimer_TimerId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionTimer");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TimerId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TimerId",
                table: "Questions");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timer",
                table: "Questions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timer",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "TimerId",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuestionTimer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timer = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTimer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TimerId",
                table: "Questions",
                column: "TimerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionTimer_TimerId",
                table: "Questions",
                column: "TimerId",
                principalTable: "QuestionTimer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
