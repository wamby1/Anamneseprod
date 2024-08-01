using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Questionnaire
    {
        [Key]
        public string? QuestionaireID { get; set; }
        public string? FhirID { get; set; }
        public string? Category { get; set; }
        public string? CodeID { get; set; }
        public Coding? Coding { get; set; }
    }
}
