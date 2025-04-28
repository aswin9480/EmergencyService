using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ResourceAvailableAPI.Models;

namespace ResourceAvailableAPI.Data
{
    public partial class ResourceContext : DbContext
    {
        public ResourceContext(DbContextOptions<ResourceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resources");

                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.ResourceId)
                      .ValueGeneratedOnAdd(); 

                entity.Property(e => e.ResourceType)
                      .HasMaxLength(100);

                entity.Property(e => e.ResourceName)
                      .HasMaxLength(100);

                entity.Property(e => e.NumberOfAvailable)
                      .IsRequired();

                entity.Property(e => e.TotalAvailable)
                      .IsRequired();

                entity.Property(e => e.ContactInfo)
                      .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
