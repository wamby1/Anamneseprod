using Microsoft.AspNetCore.Identity;

namespace Anamneseprod.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? FhirID { get; set; }
        public string? NameFamily { get; set; }
        public string? NameGiven { get; set; }
        public string? Gender { get; set; }
        public string? Birthdate { get; set; }
        public string? AddressLine { get; set; }
        public string? AddressCity { get; set; }
        public int AddressPostalCode { get; set; }
       
    }
}
