using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    IdAreas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaArea = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.IdAreas);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StockBarangs",
                columns: table => new
                {
                    IdBarang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KodeGudang = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KodeMaterial = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeskripsiMaterial = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseUnit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HargaPerUnit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBarangs", x => x.IdBarang);
                    table.UniqueConstraint("AK_StockBarangs_KodeMaterial", x => x.KodeMaterial);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sumurs",
                columns: table => new
                {
                    IdSumur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaSumur = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdAreas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sumurs", x => x.IdSumur);
                    table.ForeignKey(
                        name: "FK_Sumurs_Areas_IdAreas",
                        column: x => x.IdAreas,
                        principalTable: "Areas",
                        principalColumn: "IdAreas",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StockHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KodeMaterial = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TanggalTransaksi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StokAwal = table.Column<int>(type: "int", nullable: false),
                    BarangMasuk = table.Column<int>(type: "int", nullable: false),
                    BarangKeluar = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    StockBarangIdBarang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockHistories_StockBarangs_KodeMaterial",
                        column: x => x.KodeMaterial,
                        principalTable: "StockBarangs",
                        principalColumn: "KodeMaterial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockHistories_StockBarangs_StockBarangIdBarang",
                        column: x => x.StockBarangIdBarang,
                        principalTable: "StockBarangs",
                        principalColumn: "IdBarang");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KeperluanSumurs",
                columns: table => new
                {
                    IdKeperluans = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdSumur = table.Column<int>(type: "int", nullable: false),
                    KodeMaterial = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeskripsiMaterial = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseUnit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    StockBarangIdBarang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeperluanSumurs", x => x.IdKeperluans);
                    table.ForeignKey(
                        name: "FK_KeperluanSumurs_StockBarangs_KodeMaterial",
                        column: x => x.KodeMaterial,
                        principalTable: "StockBarangs",
                        principalColumn: "KodeMaterial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeperluanSumurs_StockBarangs_StockBarangIdBarang",
                        column: x => x.StockBarangIdBarang,
                        principalTable: "StockBarangs",
                        principalColumn: "IdBarang");
                    table.ForeignKey(
                        name: "FK_KeperluanSumurs_Sumurs_IdSumur",
                        column: x => x.IdSumur,
                        principalTable: "Sumurs",
                        principalColumn: "IdSumur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KeperluanHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdKeperluans = table.Column<int>(type: "int", nullable: false),
                    IdSumur = table.Column<int>(type: "int", nullable: false),
                    KodeMaterial = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tanggal = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    JumlahKeluar = table.Column<int>(type: "int", nullable: false),
                    StockBarangIdBarang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeperluanHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeperluanHistories_KeperluanSumurs_IdKeperluans",
                        column: x => x.IdKeperluans,
                        principalTable: "KeperluanSumurs",
                        principalColumn: "IdKeperluans",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeperluanHistories_StockBarangs_KodeMaterial",
                        column: x => x.KodeMaterial,
                        principalTable: "StockBarangs",
                        principalColumn: "KodeMaterial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeperluanHistories_StockBarangs_StockBarangIdBarang",
                        column: x => x.StockBarangIdBarang,
                        principalTable: "StockBarangs",
                        principalColumn: "IdBarang");
                    table.ForeignKey(
                        name: "FK_KeperluanHistories_Sumurs_IdSumur",
                        column: x => x.IdSumur,
                        principalTable: "Sumurs",
                        principalColumn: "IdSumur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanHistories_IdKeperluans",
                table: "KeperluanHistories",
                column: "IdKeperluans");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanHistories_IdSumur",
                table: "KeperluanHistories",
                column: "IdSumur");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanHistories_KodeMaterial",
                table: "KeperluanHistories",
                column: "KodeMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanHistories_StockBarangIdBarang",
                table: "KeperluanHistories",
                column: "StockBarangIdBarang");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanSumurs_IdSumur",
                table: "KeperluanSumurs",
                column: "IdSumur");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanSumurs_KodeMaterial",
                table: "KeperluanSumurs",
                column: "KodeMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanSumurs_StockBarangIdBarang",
                table: "KeperluanSumurs",
                column: "StockBarangIdBarang");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_KodeMaterial",
                table: "StockHistories",
                column: "KodeMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_StockBarangIdBarang",
                table: "StockHistories",
                column: "StockBarangIdBarang");

            migrationBuilder.CreateIndex(
                name: "IX_Sumurs_IdAreas",
                table: "Sumurs",
                column: "IdAreas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeperluanHistories");

            migrationBuilder.DropTable(
                name: "StockHistories");

            migrationBuilder.DropTable(
                name: "KeperluanSumurs");

            migrationBuilder.DropTable(
                name: "StockBarangs");

            migrationBuilder.DropTable(
                name: "Sumurs");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
