using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeperluanSumur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KodeMaterial",
                table: "StockHistories",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_KodeMaterial",
                table: "StockHistories",
                column: "KodeMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_StockHistories_StockBarangs_KodeMaterial",
                table: "StockHistories",
                column: "KodeMaterial",
                principalTable: "StockBarangs",
                principalColumn: "KodeMaterial",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockHistories_StockBarangs_KodeMaterial",
                table: "StockHistories");

            migrationBuilder.DropIndex(
                name: "IX_StockHistories_KodeMaterial",
                table: "StockHistories");

            migrationBuilder.AlterColumn<string>(
                name: "KodeMaterial",
                table: "StockHistories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
