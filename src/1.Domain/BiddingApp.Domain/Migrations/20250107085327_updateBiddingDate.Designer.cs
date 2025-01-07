﻿// <auto-generated />
using System;
using BiddingApp.Domain.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiddingApp.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250107085327_updateBiddingDate")]
    partial class updateBiddingDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.Bidding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BiddingAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BiddingSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("bit");

                    b.Property<decimal>("UserCurrentBidding")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BiddingSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("Biddings");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.BiddingSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("HighestBidding")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<decimal>("MinimumJumpingValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalBiddingCount")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("BiddingSessions");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Brands")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Horsepower")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("MaximumSpeed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfChairs")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TrunkCapacity")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VIN")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.Bidding", b =>
                {
                    b.HasOne("BiddingApp.Domain.Models.Entities.BiddingSession", "BiddingSession")
                        .WithMany("Biddings")
                        .HasForeignKey("BiddingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiddingApp.Domain.Models.Entities.User", "User")
                        .WithMany("Bids")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BiddingSession");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.BiddingSession", b =>
                {
                    b.HasOne("BiddingApp.Domain.Models.Entities.Vehicle", "Vehicle")
                        .WithMany("BiddingSessions")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.BiddingSession", b =>
                {
                    b.Navigation("Biddings");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("BiddingApp.Domain.Models.Entities.Vehicle", b =>
                {
                    b.Navigation("BiddingSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
