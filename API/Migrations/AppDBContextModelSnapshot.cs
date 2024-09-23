﻿// <auto-generated />
using System;
using System.Collections.Generic;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Models.API.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Role")
                        .HasColumnType("boolean");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.CompletedQuiz", b =>
                {
                    b.Property<int>("CompletedQuizID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CompletedQuizID"));

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<int>("QuizID")
                        .HasColumnType("integer");

                    b.Property<int>("Results")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("CompletedQuizID");

                    b.HasIndex("QuizID");

                    b.HasIndex("UserID");

                    b.ToTable("CompletedQuizs");
                });

            modelBuilder.Entity("API.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("QuestionID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<string[]>("PossibleAnswers")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("QuestionStatus")
                        .HasColumnType("boolean");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UnderCategory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("QuestionID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("API.Models.Quiz", b =>
                {
                    b.Property<int>("QuizID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("QuizID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("InvitedUsers")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("MainDifficulty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("QuestionAmount")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Questions")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<List<int>>("QuizAnswer")
                        .HasColumnType("integer[]");

                    b.Property<DateTime>("QuizDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Timer")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<int>>("UserAnswer")
                        .HasColumnType("integer[]");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("QuizID");

                    b.HasIndex("UserID");

                    b.ToTable("Quizs");
                });

            modelBuilder.Entity("API.Models.CompletedQuiz", b =>
                {
                    b.HasOne("API.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.API.Models.User", "User")
                        .WithMany("CompletedQuizzes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Models.Quiz", b =>
                {
                    b.HasOne("API.Models.API.Models.User", null)
                        .WithMany("CreatedQuizzes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.API.Models.User", b =>
                {
                    b.Navigation("CompletedQuizzes");

                    b.Navigation("CreatedQuizzes");
                });
#pragma warning restore 612, 618
        }
    }
}
