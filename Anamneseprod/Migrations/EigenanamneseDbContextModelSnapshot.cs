﻿// <auto-generated />
using System;
using Anamneseprod.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Anamneseprod.Migrations
{
    [DbContext(typeof(EigenanamneseDbContext))]
    partial class EigenanamneseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Anamneseprod.Models.Answer", b =>
                {
                    b.Property<string>("AnswerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnswerID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Anamneseprod.Models.Coding", b =>
                {
                    b.Property<string>("CodeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Display")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("System")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserSelected")
                        .HasColumnType("bit");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeID");

                    b.ToTable("Codings");
                });

            modelBuilder.Entity("Anamneseprod.Models.Eigenanamnese", b =>
                {
                    b.Property<string>("AnamneseID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FhirID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientdataPatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionaireID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionnaireQuestionaireID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnamneseID");

                    b.HasIndex("PatientdataPatientID");

                    b.HasIndex("QuestionnaireQuestionaireID");

                    b.ToTable("Eigenanamnesen");
                });

            modelBuilder.Entity("Anamneseprod.Models.Patientdata", b =>
                {
                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AddressPostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Birthdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FhirID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFamily")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGiven")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Anamneseprod.Models.Question", b =>
                {
                    b.Property<string>("QuestionID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodingCodeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Multichoice")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionID");

                    b.HasIndex("CodingCodeID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Anamneseprod.Models.Questionnaire", b =>
                {
                    b.Property<string>("QuestionaireID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodingCodeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FhirID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionaireID");

                    b.HasIndex("CodingCodeID");

                    b.ToTable("Questionnaires");
                });

            modelBuilder.Entity("Anamneseprod.Models.Symptom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CodeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodingCodeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CodingCodeID");

                    b.ToTable("Symptoms");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Anamneseprod.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("AddressCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AddressPostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Birthdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FhirID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFamily")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGiven")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Anamneseprod.Models.Answer", b =>
                {
                    b.HasOne("Anamneseprod.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionID");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Anamneseprod.Models.Eigenanamnese", b =>
                {
                    b.HasOne("Anamneseprod.Models.Patientdata", "Patientdata")
                        .WithMany("Eigenanamnesen")
                        .HasForeignKey("PatientdataPatientID");

                    b.HasOne("Anamneseprod.Models.Questionnaire", "Questionnaire")
                        .WithMany()
                        .HasForeignKey("QuestionnaireQuestionaireID");

                    b.Navigation("Patientdata");

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("Anamneseprod.Models.Question", b =>
                {
                    b.HasOne("Anamneseprod.Models.Coding", "Coding")
                        .WithMany("Questions")
                        .HasForeignKey("CodingCodeID");

                    b.Navigation("Coding");
                });

            modelBuilder.Entity("Anamneseprod.Models.Questionnaire", b =>
                {
                    b.HasOne("Anamneseprod.Models.Coding", "Coding")
                        .WithMany()
                        .HasForeignKey("CodingCodeID");

                    b.Navigation("Coding");
                });

            modelBuilder.Entity("Anamneseprod.Models.Symptom", b =>
                {
                    b.HasOne("Anamneseprod.Models.Coding", "Coding")
                        .WithMany("Symptoms")
                        .HasForeignKey("CodingCodeID");

                    b.Navigation("Coding");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Anamneseprod.Models.Coding", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Symptoms");
                });

            modelBuilder.Entity("Anamneseprod.Models.Patientdata", b =>
                {
                    b.Navigation("Eigenanamnesen");
                });

            modelBuilder.Entity("Anamneseprod.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
