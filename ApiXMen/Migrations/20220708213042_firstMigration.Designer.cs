﻿// <auto-generated />
using ApiXMen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiXMen.Migrations
{
    [DbContext(typeof(XMenContext))]
    [Migration("20220708213042_firstMigration")]
    partial class firstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiXMen.Models.DnaResult", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DnaVerified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TestResult")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("DnaResults");
                });
#pragma warning restore 612, 618
        }
    }
}
