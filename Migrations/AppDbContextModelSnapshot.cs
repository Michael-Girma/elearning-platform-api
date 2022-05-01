﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using elearning_platform.Data;

#nullable disable

namespace elearning_platform.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("elearning_platform.Models.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("AdminId");

                    b.HasIndex("Uid");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("elearning_platform.Models.EducationLevel", b =>
                {
                    b.Property<Guid>("EducationLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EducationLevelId");

                    b.ToTable("EducationLevels");
                });

            modelBuilder.Entity("elearning_platform.Models.InternalFileMetadata", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rev")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UploadedByUid")
                        .HasColumnType("uuid");

                    b.HasKey("FileId");

                    b.HasIndex("UploadedByUid");

                    b.ToTable("InternalFiles");
                });

            modelBuilder.Entity("elearning_platform.Models.Mfa", b =>
                {
                    b.Property<int>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Guid"));

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Iat")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PinCode")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Guid");

                    b.ToTable("MFAs");
                });

            modelBuilder.Entity("elearning_platform.Models.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EducationLevelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("StudentId");

                    b.HasIndex("EducationLevelId");

                    b.HasIndex("Uid");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("elearning_platform.Models.Subject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EducationLevelId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SubjectId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("elearning_platform.Models.TaughtSubject", b =>
                {
                    b.Property<Guid>("TaughtSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("PricePerHour")
                        .HasColumnType("real");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.Property<bool>("TaughtInPerson")
                        .HasColumnType("boolean");

                    b.Property<bool>("TaughtOnline")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uuid");

                    b.HasKey("TaughtSubjectId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TutorId");

                    b.ToTable("TaughtSubjects");
                });

            modelBuilder.Entity("elearning_platform.Models.Tutor", b =>
                {
                    b.Property<Guid>("TutorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean");

                    b.HasKey("TutorId");

                    b.HasIndex("Uid");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("elearning_platform.Models.User", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Banned")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Uid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("elearning_platform.Models.UserClaim", b =>
                {
                    b.Property<Guid>("UserClaimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Claim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserClaimId");

                    b.HasIndex("Uid");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("elearning_platform.Models.Admin", b =>
                {
                    b.HasOne("elearning_platform.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Uid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("elearning_platform.Models.InternalFileMetadata", b =>
                {
                    b.HasOne("elearning_platform.Models.User", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedByUid");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("elearning_platform.Models.Student", b =>
                {
                    b.HasOne("elearning_platform.Models.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elearning_platform.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Uid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("elearning_platform.Models.Subject", b =>
                {
                    b.HasOne("elearning_platform.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elearning_platform.Models.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("elearning_platform.Models.TaughtSubject", b =>
                {
                    b.HasOne("elearning_platform.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elearning_platform.Models.Tutor", "Tutor")
                        .WithMany()
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("elearning_platform.Models.Tutor", b =>
                {
                    b.HasOne("elearning_platform.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Uid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("elearning_platform.Models.UserClaim", b =>
                {
                    b.HasOne("elearning_platform.Models.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("Uid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("elearning_platform.Models.User", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
