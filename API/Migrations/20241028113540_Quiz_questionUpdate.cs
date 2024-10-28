using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Quiz_questionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User_Quiz",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "TimeUsed",
                table: "User_Quiz",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Quiz_Question",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Quiz",
                table: "User_Quiz",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quiz_Question",
                table: "Quiz_Question",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Quiz",
                table: "User_Quiz");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quiz_Question",
                table: "Quiz_Question");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User_Quiz");

            migrationBuilder.DropColumn(
                name: "TimeUsed",
                table: "User_Quiz");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Quiz_Question");
        }
    }
}
