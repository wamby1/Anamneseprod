using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Question
    {
        [Key]
        public string? QuestionID { get; set; }
        public string? Title { get; set; }
        public bool Multichoice { get; set; }
        public string? CodeID { get; set; }
        public Coding? Coding { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }

    }    
}
