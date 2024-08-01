using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NuGet.Packaging.Signing;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizs_Difficulty_MaindifficultyId",
                table: "Quizs");

            migrationBuilder.DropIndex(
                name: "IX_Quizs_MaindifficultyId",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "Timer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Difficulty");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Difficulty");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Quizs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Timestamp>(
                name: "Timer",
                table: "Quizs",
                type: "Timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "AddedTime",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Quizs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionAmount",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UnderCategory",
                table: "Questions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Questions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Questions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Questions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "QuestionStatus",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DifficultyLevel",
                table: "Difficulty",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 1,
                column: "DifficultyLevel",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 2,
                column: "DifficultyLevel",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 3,
                column: "DifficultyLevel",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 4,
                column: "DifficultyLevel",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 5,
                column: "DifficultyLevel",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 6,
                column: "DifficultyLevel",
                value: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedTime",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuestionAmount",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuestionStatus",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Quizs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timer",
                table: "Quizs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UnderCategory",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timer",
                table: "Questions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "DifficultyLevel",
                table: "Difficulty",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Difficulty",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Difficulty",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DifficultyLevel", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GF2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DifficultyLevel", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "H1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DifficultyLevel", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "H2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DifficultyLevel", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "H3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DifficultyLevel", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "H4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "DifficultyLevel", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "H5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_MaindifficultyId",
                table: "Quizs",
                column: "MaindifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizs_Difficulty_MaindifficultyId",
                table: "Quizs",
                column: "MaindifficultyId",
                principalTable: "Difficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
