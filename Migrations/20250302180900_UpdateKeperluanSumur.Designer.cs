﻿// <auto-generated />
using System;
using InventoryManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagement.Migrations
{
    [DbContext(typeof(InventoryContext))]
    [Migration("20250302180900_UpdateKeperluanSumur")]
    partial class UpdateKeperluanSumur
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("InventoryManagement.Models.KeperluanSumur", b =>
                {
                    b.Property<int>("IdKeperluans")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdKeperluans"));

                    b.Property<string>("BaseUnit")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("DeskripsiMaterial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("IdSumur")
                        .HasColumnType("int");

                    b.Property<int>("Jumlah")
                        .HasColumnType("int");

                    b.Property<string>("KodeMaterial")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdKeperluans");

                    b.HasIndex("IdSumur");

                    b.HasIndex("KodeMaterial");

                    b.ToTable("KeperluanSumurs");
                });

            modelBuilder.Entity("InventoryManagement.Models.StockBarang", b =>
                {
                    b.Property<int>("IdBarang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdBarang"));

                    b.Property<string>("BaseUnit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DeskripsiMaterial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HargaPerUnit")
                        .HasColumnType("int");

                    b.Property<int>("Jumlah")
                        .HasColumnType("int");

                    b.Property<string>("KodeGudang")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("KodeMaterial")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdBarang");

                    b.ToTable("StockBarangs");
                });

            modelBuilder.Entity("InventoryManagement.Models.StockHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int>("BarangKeluar")
                        .HasColumnType("int");

                    b.Property<int>("BarangMasuk")
                        .HasColumnType("int");

                    b.Property<string>("KodeMaterial")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("StokAwal")
                        .HasColumnType("int");

                    b.Property<DateTime>("TanggalTransaksi")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("KodeMaterial");

                    b.ToTable("StockHistories");
                });

            modelBuilder.Entity("InventoryManagement.Models.Sumur", b =>
                {
                    b.Property<int>("IdSumur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdSumur"));

                    b.Property<string>("DaerahSumur")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NamaSumur")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdSumur");

                    b.ToTable("Sumurs");
                });

            modelBuilder.Entity("InventoryManagement.Models.KeperluanSumur", b =>
                {
                    b.HasOne("InventoryManagement.Models.Sumur", "Sumur")
                        .WithMany("KeperluanSumurs")
                        .HasForeignKey("IdSumur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Models.StockBarang", "StockBarang")
                        .WithMany()
                        .HasForeignKey("KodeMaterial")
                        .HasPrincipalKey("KodeMaterial")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("StockBarang");

                    b.Navigation("Sumur");
                });

            modelBuilder.Entity("InventoryManagement.Models.StockHistory", b =>
                {
                    b.HasOne("InventoryManagement.Models.StockBarang", "StockBarang")
                        .WithMany()
                        .HasForeignKey("KodeMaterial")
                        .HasPrincipalKey("KodeMaterial")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StockBarang");
                });

            modelBuilder.Entity("InventoryManagement.Models.Sumur", b =>
                {
                    b.Navigation("KeperluanSumurs");
                });
#pragma warning restore 612, 618
        }
    }
}
