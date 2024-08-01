using System.ComponentModel.DataAnnotations;

namespace Anamneseprod.Models
{
    public class Patientdata
    {
        [Key]
        public string? PatientID { get; set; }
        public string? FhirID { get; set; }
        public string? NameFamily { get; set; }
        public string? NameGiven { get; set; }
        public string? Gender { get; set; }
        public string? Birthdate { get; set; }
        public string? AddressLine { get; set; }
        public string? AddressCity { get; set; }
        public int AddressPostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public virtual ICollection<Eigenanamnese>? Eigenanamnesen { get; set; }

    }
}
