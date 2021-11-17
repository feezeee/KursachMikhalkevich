using KursachMikhalkevich.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AccessRight> AccessRights { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PartnerCompany> PartnerCompanies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Group>()
                .HasMany(c => c.Subjects)
                .WithMany(s => s.Groups)
                .UsingEntity<SubjectGroup>(
                   j => j
                    .HasOne(pt => pt.Subject)
                    .WithMany(t => t.SubjectsGroups)
                    .HasForeignKey(pt => pt.SubjectId),
                j => j
                    .HasOne(pt => pt.Group)
                    .WithMany(p => p.SubjectsGroups)
                    .HasForeignKey(pt => pt.GroupId),
                j =>
                {
                    j.HasKey(t => new { t.GroupId, t.SubjectId });
                    j.ToTable("subjects_has_groups");
                });
        }
    }
}
