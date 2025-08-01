﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Adapter.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Adapter.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.Projects.Entities.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("Description");

                    b.Property<decimal?>("EstimatedHours")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("EstimatedHours");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal?>("LoggedHours")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("LoggedHours");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("Status", "Core.Domain.Projects.Entities.Assignment.Status#AssignmentStatus", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Status");
                        });

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("Core.Domain.Projects.Entities.ProjectUser", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProjectId");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("Core.Domain.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime");

                    b.ComplexProperty<Dictionary<string, object>>("ProjectPeriod", "Core.Domain.Projects.Project.ProjectPeriod#DateRange", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime")
                                .HasColumnName("StartDate");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Status", "Core.Domain.Projects.Project.Status#ProjectStatus", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Status");
                        });

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Core.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime");

                    b.Property<int>("UserRole")
                        .HasColumnType("int")
                        .HasColumnName("UserRole");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "Core.Domain.Users.User.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");
                        });

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("ProjectId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Core.Domain.Projects.Entities.Assignment", b =>
                {
                    b.HasOne("Core.Domain.Projects.Project", "Project")
                        .WithMany("Assignments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Users.User", "User")
                        .WithMany("Assignments")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Projects.Entities.ProjectUser", b =>
                {
                    b.HasOne("Core.Domain.Projects.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Users.User", "User")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Users.User", b =>
                {
                    b.HasOne("Core.Domain.Projects.Project", null)
                        .WithMany("Users")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Core.Domain.Projects.Project", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("ProjectUsers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Core.Domain.Users.User", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("ProjectUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
