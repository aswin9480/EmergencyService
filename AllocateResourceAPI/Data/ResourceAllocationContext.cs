using Microsoft.EntityFrameworkCore;
using AllocateResourceAPI.Models;

namespace AllocateResourceAPI.Data
{
    public partial class ResourceAllocationContext : DbContext
    {
        public ResourceAllocationContext(DbContextOptions<ResourceAllocationContext> options)
            : base(options)
        {
        }

        // DbSet for ResourceAllocated table
        public virtual DbSet<ResourceAllocated> ResourceAllocatedTbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceAllocated>(entity =>
            {
                // Primary Key Constraint
                entity.HasKey(e => e.AllocationId).HasName("PK_ResourceAllocated_AllocationId");

                // Table Name
                entity.ToTable("ResourceAllocatedTbl");

                // Property Configurations
                entity.Property(e => e.AllocationId)
                      .ValueGeneratedOnAdd(); // Auto-increment for AllocationId

                entity.Property(e => e.IncidentId) // Adding IncidentId configuration
                      .IsRequired()  // Assuming IncidentId is mandatory; adjust if optional
                      .HasColumnType("int"); // Integer for IncidentId

                entity.Property(e => e.IncidentType)
                      .HasMaxLength(50); // Max Length for Incident Type

                entity.Property(e => e.Severity)
                      .HasMaxLength(20); // Max Length for Severity

                entity.Property(e => e.Location)
                      .HasMaxLength(100); // Max Length for Location
                entity.Property(e => e.ResourceId)
                       .IsRequired()
                       .HasColumnType("int");

                entity.Property(e => e.ResourceName)
                      .HasMaxLength(100); // Max Length for Resource Name

                entity.Property(e => e.QuantityAllocated)
                      .HasColumnType("int"); // Integer for Quantity Allocated
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
