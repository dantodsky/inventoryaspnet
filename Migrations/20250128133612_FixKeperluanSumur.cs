using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixKeperluanSumur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POBarangs");

            migrationBuilder.DropColumn(
                name: "TotalHargaBarang",
                table: "StockBarangs");

            migrationBuilder.AlterColumn<string>(
                name: "KodeMaterial",
                table: "StockBarangs",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "KodeMaterial",
                table: "KeperluanSumurs",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StockBarangs_KodeMaterial",
                table: "StockBarangs",
                column: "KodeMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_KeperluanSumurs_KodeMaterial",
                table: "KeperluanSumurs",
                column: "KodeMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_KeperluanSumurs_StockBarangs_KodeMaterial",
                table: "KeperluanSumurs",
                column: "KodeMaterial",
                principalTable: "StockBarangs",
                principalColumn: "KodeMaterial",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeperluanSumurs_StockBarangs_KodeMaterial",
                table: "KeperluanSumurs");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StockBarangs_KodeMaterial",
                table: "StockBarangs");

            migrationBuilder.DropIndex(
                name: "IX_KeperluanSumurs_KodeMaterial",
                table: "KeperluanSumurs");

            migrationBuilder.AlterColumn<string>(
                name: "KodeMaterial",
                table: "StockBarangs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TotalHargaBarang",
                table: "StockBarangs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "KodeMaterial",
                table: "KeperluanSumurs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "POBarangs",
                columns: table => new
                {
                    IdPOBarang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BaseUnit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeskripsiMaterial = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EtaDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HargaPerUnit = table.Column<int>(type: "int", nullable: false),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    KodeGudang = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KodeMaterial = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KodeRO = table.Column<int>(type: "int", nullable: false),
                    Vendor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POBarangs", x => x.IdPOBarang);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
