﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResourceAvailableAPI.Data;

#nullable disable

namespace ResourceAvailableAPI.Migrations
{
    [DbContext(typeof(ResourceContext))]
    [Migration("20250423044346_yo2")]
    partial class yo2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ResourceAvailableAPI.Models.Resource", b =>
                {
                    b.Property<int>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResourceId"));

                    b.Property<string>("ContactInfo")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NumberOfAvailable")
                        .HasColumnType("int");

                    b.Property<string>("ResourceName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ResourceType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TotalAvailable")
                        .HasColumnType("int");

                    b.HasKey("ResourceId");

                    b.ToTable("Resources", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
