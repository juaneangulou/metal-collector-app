﻿// <auto-generated />
using System;
using MetalCollector.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetalCollector.WebApi.Migrations
{
    [DbContext(typeof(MetalCollectorDbContext))]
    [Migration("20231209231258_NewFieldsOptionalINItems")]
    partial class NewFieldsOptionalINItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MetalCollector.WebApi.Models.Artist", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Active")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Formed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MAArtistId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Band", b =>
                {
                    b.Property<string>("BandId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Formed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MABandId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BandId");

                    b.ToTable("Bands");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Discography", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MADiscographyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Discographies");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BuyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MAArtistId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Lineup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Instrument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MALineupId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Lineups");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Member", b =>
                {
                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BandId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Instrument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MAMemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberId");

                    b.HasIndex("BandId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Release", b =>
                {
                    b.Property<string>("ReleaseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BandId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MAReleaseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReleaseId");

                    b.HasIndex("BandId");

                    b.ToTable("Releases");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Social", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MASocialId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Media")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Socials");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Discography", b =>
                {
                    b.HasOne("MetalCollector.WebApi.Models.Artist", "Artist")
                        .WithMany("Discographies")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Lineup", b =>
                {
                    b.HasOne("MetalCollector.WebApi.Models.Artist", "Artist")
                        .WithMany("Lineup")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Member", b =>
                {
                    b.HasOne("MetalCollector.WebApi.Models.Band", null)
                        .WithMany("Lineup")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Release", b =>
                {
                    b.HasOne("MetalCollector.WebApi.Models.Band", null)
                        .WithMany("Discography")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Social", b =>
                {
                    b.HasOne("MetalCollector.WebApi.Models.Artist", "Artist")
                        .WithMany("Social")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Artist", b =>
                {
                    b.Navigation("Discographies");

                    b.Navigation("Lineup");

                    b.Navigation("Social");
                });

            modelBuilder.Entity("MetalCollector.WebApi.Models.Band", b =>
                {
                    b.Navigation("Discography");

                    b.Navigation("Lineup");
                });
#pragma warning restore 612, 618
        }
    }
}
