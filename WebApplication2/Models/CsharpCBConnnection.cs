using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CsharpCBContext : DbContext
    {
        public CsharpCBContext() : base("CsDBConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;

        }
        public DbSet<Project> Project { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Member> Member { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>()
               .Property(m => m.ID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Members)
                .WithMany(m => m.Projects) 
                .Map(m =>
                {
                    m.ToTable("ProjectMembers"); 
                    m.MapLeftKey("ProjectId");
                    m.MapRightKey("MemberId");
                });
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOptional()
                .WillCascadeOnDelete(true);
        }
    }
}