﻿// <auto-generated />
using System;
using CitiesWebAPI.Models.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CitiesWebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181120045348_CitiesWebAPI")]
    partial class CitiesWebAPI
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CitiesWebAPI.Models.Attraction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<Guid?>("CityId1");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CityId1");

                    b.ToTable("Attractions");
                });

            modelBuilder.Entity("CitiesWebAPI.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("CitiesWebAPI.Models.Attraction", b =>
                {
                    b.HasOne("CitiesWebAPI.Models.City")
                        .WithMany("attractions")
                        .HasForeignKey("CityId1");
                });
#pragma warning restore 612, 618
        }
    }
}
