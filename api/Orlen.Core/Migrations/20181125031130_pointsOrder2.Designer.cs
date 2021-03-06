﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orlen.Core;

namespace Orlen.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181125031130_pointsOrder2")]
    partial class pointsOrder2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Orlen.Core.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IssueTypeId");

                    b.Property<int?>("PointId");

                    b.Property<int?>("SectionId");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("Id");

                    b.HasIndex("IssueTypeId");

                    b.HasIndex("PointId");

                    b.HasIndex("SectionId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Orlen.Core.Entities.IssueType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("IssueTypes");
                });

            modelBuilder.Entity("Orlen.Core.Entities.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsGate");

                    b.Property<decimal>("Latitude")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("Longitude")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("Orlen.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Orlen.Core.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Height");

                    b.Property<decimal>("Length");

                    b.Property<decimal>("Weight");

                    b.Property<decimal>("Width");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Orlen.Core.Entities.RoutePoints", b =>
                {
                    b.Property<int>("RouteId");

                    b.Property<int>("PointId");

                    b.Property<int>("Order");

                    b.HasKey("RouteId", "PointId");

                    b.HasIndex("PointId");

                    b.ToTable("RoutePoints");
                });

            modelBuilder.Entity("Orlen.Core.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndId");

                    b.Property<string>("Name");

                    b.Property<int>("StartId");

                    b.HasKey("Id");

                    b.HasIndex("EndId");

                    b.HasIndex("StartId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Orlen.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Orlen.Core.Entities.Issue", b =>
                {
                    b.HasOne("Orlen.Core.Entities.IssueType", "IssueType")
                        .WithMany()
                        .HasForeignKey("IssueTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Orlen.Core.Entities.Point", "Point")
                        .WithMany("Issues")
                        .HasForeignKey("PointId");

                    b.HasOne("Orlen.Core.Entities.Section", "Section")
                        .WithMany("Issues")
                        .HasForeignKey("SectionId");
                });

            modelBuilder.Entity("Orlen.Core.Entities.RoutePoints", b =>
                {
                    b.HasOne("Orlen.Core.Entities.Point", "Point")
                        .WithMany("RoutePoints")
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Orlen.Core.Entities.Route", "Route")
                        .WithMany("RoutePoints")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Orlen.Core.Entities.Section", b =>
                {
                    b.HasOne("Orlen.Core.Entities.Point", "End")
                        .WithMany()
                        .HasForeignKey("EndId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Orlen.Core.Entities.Point", "Start")
                        .WithMany()
                        .HasForeignKey("StartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Orlen.Core.Entities.User", b =>
                {
                    b.HasOne("Orlen.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
