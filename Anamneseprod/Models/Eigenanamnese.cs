
using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Eigenanamnese
    {
        [Key]
        public string? AnamneseID { get; set; }
        public string? FhirID { get; set; }
        public string? PatientID { get; set; }
        public string? QuestionaireID { get; set; }
		public string? description { get; set; }
		public Questionnaire? Questionnaire { get; set; }
        public Patientdata? Patientdata { get; set; }

    }
}
