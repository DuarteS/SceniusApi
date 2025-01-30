﻿// <auto-generated />
using System;
using CalculatorApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CalculatorApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250130190053_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("CalculatorApi.Models.Calculation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Result")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Calculations");
                });
#pragma warning restore 612, 618
        }
    }
}
