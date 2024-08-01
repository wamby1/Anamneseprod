﻿// <auto-generated />
using Anamneseprod.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Anamneseprod.Migrations
{
    [DbContext(typeof(EigenanamneseDbContext))]
    [Migration("20240729084410_modelerzeugung")]
    partial class modelerzeugung
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
