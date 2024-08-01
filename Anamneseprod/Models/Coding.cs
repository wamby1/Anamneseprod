using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Coding
    {
        [Key]
        public string? CodeID { get; set; }
        public string? System { get; set; }
        public string? Version { get; set; }
        public string? Code { get; set; }
        public string? Display { get; set; }
        public bool UserSelected { get; set; }
        public string? Text { get; set; }
        public string? Category { get; set; }
        public virtual ICollection<Symptom>? Symptoms { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
    }
}
