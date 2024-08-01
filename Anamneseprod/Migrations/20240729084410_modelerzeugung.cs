using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamneseprod.Migrations
{
    /// <inheritdoc />
    public partial class modelerzeugung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Codings",
                columns: table => new
                {
                    CodeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    System = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserSelected = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codings", x => x.CodeID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FhirID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressPostalCode = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    QuestionaireID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FhirID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingCodeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.QuestionaireID);
                    table.ForeignKey(
                        name: "FK_Questionnaires_Codings_CodingCodeID",
                        column: x => x.CodingCodeID,
                        principalTable: "Codings",
                        principalColumn: "CodeID");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Multichoice = table.Column<bool>(type: "bit", nullable: false),
                    CodeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingCodeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Codings_CodingCodeID",
                        column: x => x.CodingCodeID,
                        principalTable: "Codings",
                        principalColumn: "CodeID");
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingCodeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Symptoms_Codings_CodingCodeID",
                        column: x => x.CodingCodeID,
                        principalTable: "Codings",
                        principalColumn: "CodeID");
                });

            migrationBuilder.CreateTable(
                name: "Eigenanamnesen",
                columns: table => new
                {
                    AnamneseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FhirID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionaireID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionnaireQuestionaireID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientdataPatientID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eigenanamnesen", x => x.AnamneseID);
                    table.ForeignKey(
                        name: "FK_Eigenanamnesen_Patients_PatientdataPatientID",
                        column: x => x.PatientdataPatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID");
                    table.ForeignKey(
                        name: "FK_Eigenanamnesen_Questionnaires_QuestionnaireQuestionaireID",
                        column: x => x.QuestionnaireQuestionaireID,
                        principalTable: "Questionnaires",
                        principalColumn: "QuestionaireID");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Eigenanamnesen_PatientdataPatientID",
                table: "Eigenanamnesen",
                column: "PatientdataPatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Eigenanamnesen_QuestionnaireQuestionaireID",
                table: "Eigenanamnesen",
                column: "QuestionnaireQuestionaireID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_CodingCodeID",
                table: "Questionnaires",
                column: "CodingCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CodingCodeID",
                table: "Questions",
                column: "CodingCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_CodingCodeID",
                table: "Symptoms",
                column: "CodingCodeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Eigenanamnesen");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Codings");
        }
    }
}
