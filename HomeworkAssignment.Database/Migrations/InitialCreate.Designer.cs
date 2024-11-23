﻿// <auto-generated />
using System;
using HomeAssignment.Database.Contexts.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeAssignment.Database.Migrations
{
    [DbContext(typeof(HomeworkAssignmentDbContext))]
    [Migration("20241014183741_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.AssignmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AttemptCompilationMaxScore")
                        .HasColumnType("integer");

                    b.Property<int>("AttemptCompilationMinScore")
                        .HasColumnType("integer");

                    b.Property<bool>("AttemptCompilationSectionEnable")
                        .HasColumnType("boolean");

                    b.Property<int>("AttemptQualityMaxScore")
                        .HasColumnType("integer");

                    b.Property<int>("AttemptQualityMinScore")
                        .HasColumnType("integer");

                    b.Property<bool>("AttemptQualitySectionEnable")
                        .HasColumnType("boolean");

                    b.Property<int>("AttemptTestsMaxScore")
                        .HasColumnType("integer");

                    b.Property<int>("AttemptTestsMinScore")
                        .HasColumnType("integer");

                    b.Property<bool>("AttemptTestsSectionEnable")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("MaxAttemptsAmount")
                        .HasColumnType("integer");

                    b.Property<int>("MaxScore")
                        .HasColumnType("integer");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("AssignmentEntities");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.AttemptEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("AttemptNumber")
                        .HasColumnType("integer");

                    b.Property<int>("CompilationScore")
                        .HasColumnType("integer");

                    b.Property<int>("FinalScore")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("QualityScore")
                        .HasColumnType("integer");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<int>("TestsScore")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("StudentId");

                    b.ToTable("AttemptEntities");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.GitHubProfilesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("GithubAccessToken")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("GithubPictureUrl")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("GithubProfileUrl")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("GithubUsername")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("GitHubProfilesEntities");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.AssignmentEntity", b =>
                {
                    b.HasOne("HomeworkAssignment.Database.Entities.GitHubProfilesEntity", "OwnerEntity")
                        .WithMany("Assignments")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("OwnerEntity");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.AttemptEntity", b =>
                {
                    b.HasOne("HomeworkAssignment.Database.Entities.AssignmentEntity", "Assignment")
                        .WithMany("Attempts")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeworkAssignment.Database.Entities.GitHubProfilesEntity", "Student")
                        .WithMany("Attempts")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.GitHubProfilesEntity", b =>
                {
                    b.HasOne("HomeworkAssignment.Database.Entities.UserEntity", "User")
                        .WithMany("GitHubProfiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.AssignmentEntity", b =>
                {
                    b.Navigation("Attempts");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.GitHubProfilesEntity", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Attempts");
                });

            modelBuilder.Entity("HomeworkAssignment.Database.Entities.UserEntity", b =>
                {
                    b.Navigation("GitHubProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}