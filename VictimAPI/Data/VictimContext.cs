using System;
using System.Collections.Generic;
using global::VictimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace VictimAPI.Data
{
    public partial class VictimContext : DbContext
    {
        public VictimContext(DbContextOptions<VictimContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IncidentTbl> IncidentTbls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncidentTbl>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Incident_Id"); // Simplified primary key constraint name

                entity.ToTable("IncidentTbl");

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // This makes the Id auto-increment

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.IncidentType).HasMaxLength(50);
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Severity).HasMaxLength(20);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IncidentDateTime).HasColumnType("datetime");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
