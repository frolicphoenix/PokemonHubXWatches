﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonHubXWatches.Data;

#nullable disable

namespace PokemonHubXWatches.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241128025757_addednewdata")]
    partial class addednewdata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PokemonHubXWatches.Models.Build", b =>
                {
                    b.Property<int>("BuildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildId"));

                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("PokemonUpdatedAttack")
                        .HasColumnType("int");

                    b.Property<int>("PokemonUpdatedCDR")
                        .HasColumnType("int");

                    b.Property<int>("PokemonUpdatedDefense")
                        .HasColumnType("int");

                    b.Property<int>("PokemonUpdatedHP")
                        .HasColumnType("int");

                    b.Property<int>("PokemonUpdatedSpAttack")
                        .HasColumnType("int");

                    b.Property<int>("PokemonUpdatedSpDefense")
                        .HasColumnType("int");

                    b.HasKey("BuildId");

                    b.HasIndex("PokemonId");

                    b.ToTable("Builds");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.BuildHeldItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuildId")
                        .HasColumnType("int");

                    b.Property<int>("HeldItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildId");

                    b.HasIndex("HeldItemId");

                    b.ToTable("BuildHeldItems");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.HeldItem", b =>
                {
                    b.Property<int>("HeldItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HeldItemId"));

                    b.Property<int>("HeldItemAttack")
                        .HasColumnType("int");

                    b.Property<int>("HeldItemCDR")
                        .HasColumnType("int");

                    b.Property<int>("HeldItemDefense")
                        .HasColumnType("int");

                    b.Property<int>("HeldItemHP")
                        .HasColumnType("int");

                    b.Property<string>("HeldItemImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeldItemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("HeldItemSpAttack")
                        .HasColumnType("int");

                    b.Property<int>("HeldItemSpDefense")
                        .HasColumnType("int");

                    b.HasKey("HeldItemId");

                    b.ToTable("HeldItems");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Pokemon", b =>
                {
                    b.Property<int>("PokemonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PokemonId"));

                    b.Property<int>("PokemonAttack")
                        .HasColumnType("int");

                    b.Property<int>("PokemonCDR")
                        .HasColumnType("int");

                    b.Property<int>("PokemonDefense")
                        .HasColumnType("int");

                    b.Property<int>("PokemonHP")
                        .HasColumnType("int");

                    b.Property<string>("PokemonImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PokemonRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PokemonSpAttack")
                        .HasColumnType("int");

                    b.Property<int>("PokemonSpDefense")
                        .HasColumnType("int");

                    b.Property<string>("PokemonStyle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WatchID")
                        .HasColumnType("int");

                    b.HasKey("PokemonId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationID"));

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WatchID")
                        .HasColumnType("int");

                    b.HasKey("ReservationID");

                    b.HasIndex("UserId");

                    b.HasIndex("WatchID")
                        .IsUnique();

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Watch", b =>
                {
                    b.Property<int>("WatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WatchID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PokemonID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("WatchID");

                    b.HasIndex("PokemonID")
                        .IsUnique()
                        .HasFilter("[PokemonID] IS NOT NULL");

                    b.ToTable("Watches");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Build", b =>
                {
                    b.HasOne("PokemonHubXWatches.Models.Pokemon", "Pokemon")
                        .WithMany("Builds")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.BuildHeldItem", b =>
                {
                    b.HasOne("PokemonHubXWatches.Models.Build", "Build")
                        .WithMany("HeldItems")
                        .HasForeignKey("BuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonHubXWatches.Models.HeldItem", "HeldItem")
                        .WithMany("Builds")
                        .HasForeignKey("HeldItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Build");

                    b.Navigation("HeldItem");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Reservation", b =>
                {
                    b.HasOne("PokemonHubXWatches.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonHubXWatches.Models.Watch", "Watch")
                        .WithOne("Reservation")
                        .HasForeignKey("PokemonHubXWatches.Models.Reservation", "WatchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Watch");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Watch", b =>
                {
                    b.HasOne("PokemonHubXWatches.Models.Pokemon", "ThemedPokemon")
                        .WithOne("ThemedWatch")
                        .HasForeignKey("PokemonHubXWatches.Models.Watch", "PokemonID");

                    b.Navigation("ThemedPokemon");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Build", b =>
                {
                    b.Navigation("HeldItems");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.HeldItem", b =>
                {
                    b.Navigation("Builds");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Pokemon", b =>
                {
                    b.Navigation("Builds");

                    b.Navigation("ThemedWatch");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.User", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PokemonHubXWatches.Models.Watch", b =>
                {
                    b.Navigation("Reservation");
                });
#pragma warning restore 612, 618
        }
    }
}
