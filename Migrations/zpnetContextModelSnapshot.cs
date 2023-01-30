﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zpnet.Models;

#nullable disable

namespace zpnet.Migrations
{
    [DbContext(typeof(zpnetContext))]
    partial class zpnetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ksiazkaprzepis", b =>
                {
                    b.Property<int>("ksiazkiid")
                        .HasColumnType("int");

                    b.Property<int>("przepisyId")
                        .HasColumnType("int");

                    b.HasKey("ksiazkiid", "przepisyId");

                    b.HasIndex("przepisyId");

                    b.ToTable("ksiazkaprzepis");
                });

            modelBuilder.Entity("zpnet.Models.autor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime?>("Data_u")
                        .HasColumnType("datetime2");

                    b.Property<string>("imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("autorzy");
                });

            modelBuilder.Entity("zpnet.Models.ksiazka", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int?>("autorid")
                        .HasColumnType("int");

                    b.Property<string>("dataStworzenia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("autorid");

                    b.ToTable("ksiazki");
                });

            modelBuilder.Entity("zpnet.Models.przepis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("autorid")
                        .HasColumnType("int");

                    b.Property<string>("nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("przepisText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("typ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("autorid");

                    b.ToTable("przepisy");
                });

            modelBuilder.Entity("ksiazkaprzepis", b =>
                {
                    b.HasOne("zpnet.Models.ksiazka", null)
                        .WithMany()
                        .HasForeignKey("ksiazkiid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zpnet.Models.przepis", null)
                        .WithMany()
                        .HasForeignKey("przepisyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("zpnet.Models.ksiazka", b =>
                {
                    b.HasOne("zpnet.Models.autor", "autor")
                        .WithMany("ksiazki")
                        .HasForeignKey("autorid");

                    b.Navigation("autor");
                });

            modelBuilder.Entity("zpnet.Models.przepis", b =>
                {
                    b.HasOne("zpnet.Models.autor", "autor")
                        .WithMany("przepisy")
                        .HasForeignKey("autorid");

                    b.Navigation("autor");
                });

            modelBuilder.Entity("zpnet.Models.autor", b =>
                {
                    b.Navigation("ksiazki");

                    b.Navigation("przepisy");
                });
#pragma warning restore 612, 618
        }
    }
}
