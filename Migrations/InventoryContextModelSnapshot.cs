﻿// <auto-generated />
using System;
using InventoryManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagement.Migrations
{
    [DbContext(typeof(InventoryContext))]
    partial class InventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("InventoryManagement.Models.Areas", b =>
                {
                    b.Property<int>("IdAreas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdAreas"));

                    b.Property<string>("NamaArea")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdAreas");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("InventoryManagement.Models.KeperluanHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdKeperluans")
                        .HasColumnType("int");

                    b.Property<int>("IdSumur")
                        .HasColumnType("int");

                    b.Property<int>("JumlahKeluar")
                        .HasColumnType("int");

                    b.Property<string>("KodeMaterial")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("StockBarangIdBarang")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdKeperluans");

                    b.HasIndex("IdSumur");

                    b.HasIndex("KodeMaterial");

                    b.HasIndex("StockBarangIdBarang");

                    b.ToTable("KeperluanHistories");
                });

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

                    b.Property<int?>("StockBarangIdBarang")
                        .HasColumnType("int");

                    b.HasKey("IdKeperluans");

                    b.HasIndex("IdSumur");

                    b.HasIndex("KodeMaterial");

                    b.HasIndex("StockBarangIdBarang");

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

                    b.Property<int?>("StockBarangIdBarang")
                        .HasColumnType("int");

                    b.Property<int>("StokAwal")
                        .HasColumnType("int");

                    b.Property<DateTime>("TanggalTransaksi")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("KodeMaterial");

                    b.HasIndex("StockBarangIdBarang");

                    b.ToTable("StockHistories");
                });

            modelBuilder.Entity("InventoryManagement.Models.Sumur", b =>
                {
                    b.Property<int>("IdSumur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdSumur"));

                    b.Property<int>("IdAreas")
                        .HasColumnType("int");

                    b.Property<string>("NamaSumur")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdSumur");

                    b.HasIndex("IdAreas");

                    b.ToTable("Sumurs");
                });

            modelBuilder.Entity("InventoryManagement.Models.KeperluanHistory", b =>
                {
                    b.HasOne("InventoryManagement.Models.KeperluanSumur", "KeperluanSumur")
                        .WithMany()
                        .HasForeignKey("IdKeperluans")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Models.Sumur", "Sumur")
                        .WithMany()
                        .HasForeignKey("IdSumur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Models.StockBarang", "StockBarang")
                        .WithMany()
                        .HasForeignKey("KodeMaterial")
                        .HasPrincipalKey("KodeMaterial")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagement.Models.StockBarang", null)
                        .WithMany("KeperluanHistories")
                        .HasForeignKey("StockBarangIdBarang");

                    b.Navigation("KeperluanSumur");

                    b.Navigation("StockBarang");

                    b.Navigation("Sumur");
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

                    b.HasOne("InventoryManagement.Models.StockBarang", null)
                        .WithMany("KeperluanSumurs")
                        .HasForeignKey("StockBarangIdBarang");

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

                    b.HasOne("InventoryManagement.Models.StockBarang", null)
                        .WithMany("StockHistories")
                        .HasForeignKey("StockBarangIdBarang");

                    b.Navigation("StockBarang");
                });

            modelBuilder.Entity("InventoryManagement.Models.Sumur", b =>
                {
                    b.HasOne("InventoryManagement.Models.Areas", "Area")
                        .WithMany("Sumurs")
                        .HasForeignKey("IdAreas")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("InventoryManagement.Models.Areas", b =>
                {
                    b.Navigation("Sumurs");
                });

            modelBuilder.Entity("InventoryManagement.Models.StockBarang", b =>
                {
                    b.Navigation("KeperluanHistories");

                    b.Navigation("KeperluanSumurs");

                    b.Navigation("StockHistories");
                });

            modelBuilder.Entity("InventoryManagement.Models.Sumur", b =>
                {
                    b.Navigation("KeperluanSumurs");
                });
#pragma warning restore 612, 618
        }
    }
}
