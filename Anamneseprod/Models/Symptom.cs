using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Symptom
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodeID { get; set; }
        public Coding? Coding { get; set; }
    }
}
