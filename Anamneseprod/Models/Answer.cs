
using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Answer
    {
        [Key]
        public string? AnswerID { get; set; }
        public string? Value { get; set; }
        public string? QuestionID { get; set; }
        public Question? Question { get; set; }
    }
}
