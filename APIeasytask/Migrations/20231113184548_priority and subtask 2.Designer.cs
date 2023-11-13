﻿// <auto-generated />
using System;
using APIeasytask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIeasytask.Migrations
{
    [DbContext(typeof(APIDbContext))]
    [Migration("20231113184548_priority and subtask 2")]
    partial class priorityandsubtask2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIeasytask.Models.Priority", b =>
                {
                    b.Property<int>("PriorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriorityId"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PriorityId");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("APIeasytask.Models.Substask", b =>
                {
                    b.Property<int>("SubtaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubtaskId"));

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SubtaskId");

                    b.ToTable("Substasks");
                });

            modelBuilder.Entity("APIeasytask.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TaskId");

                    b.ToTable("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
