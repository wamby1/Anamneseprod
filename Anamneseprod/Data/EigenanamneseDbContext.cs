using Anamneseprod.Models;
using Microsoft.EntityFrameworkCore;

namespace Anamneseprod.Data
{
    public class EigenanamneseDbContext : DbContext
    {
        public EigenanamneseDbContext(DbContextOptions<EigenanamneseDbContext> options) : base(options)
        {

        }

        public DbSet<Patientdata> Patients { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Eigenanamnese> Eigenanamnesen { get; set; }
        public DbSet<Coding> Codings { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}