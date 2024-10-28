using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class QuizUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Quiz",
                table: "User_Quiz");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quiz_Question",
                table: "Quiz_Question");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "User_Quiz");

            migrationBuilder.DropColumn(
                name: "QuestionAmount",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Quiz_Question");

            migrationBuilder.AlterColumn<string>(
                name: "HashedPassword",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HashedPassword",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "User_Quiz",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "QuestionAmount",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Quiz_Question",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Quiz",
                table: "User_Quiz",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quiz_Question",
                table: "Quiz_Question",
                column: "ID");
        }
    }
}
