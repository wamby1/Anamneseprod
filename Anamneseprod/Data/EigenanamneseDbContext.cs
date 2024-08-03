using Anamneseprod.Models;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Anamneseprod.Data
{
    public class EigenanamneseDbContext : IdentityDbContext<IdentityUser>
    {
        public EigenanamneseDbContext(DbContextOptions<EigenanamneseDbContext> options) : base(options)
        {

        }

        public DbSet<Patientdata> Patients { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Eigenanamnese> Eigenanamnesen { get; set; }
        public DbSet<Models.Coding> Codings { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Models.Questionnaire> Questionnaires { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}