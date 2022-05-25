using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kitapcilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SanalMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitapcilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kitaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklamasi = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    YayımTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirimFiyati = table.Column<double>(type: "float", nullable: false),
                    StokMiktari = table.Column<int>(type: "int", nullable: false),
                    TurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kitaplar_Turler_TurId",
                        column: x => x.TurId,
                        principalTable: "Turler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KitapKitapcilar",
                columns: table => new
                {
                    KitapId = table.Column<int>(type: "int", nullable: false),
                    KitapciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapKitapcilar", x => new { x.KitapId, x.KitapciId });
                    table.ForeignKey(
                        name: "FK_KitapKitapcilar_Kitapcilar_KitapciId",
                        column: x => x.KitapciId,
                        principalTable: "Kitapcilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KitapKitapcilar_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitapKitapcilar_KitapciId",
                table: "KitapKitapcilar",
                column: "KitapciId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_TurId",
                table: "Kitaplar",
                column: "TurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KitapKitapcilar");

            migrationBuilder.DropTable(
                name: "Kitapcilar");

            migrationBuilder.DropTable(
                name: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Turler");
        }
    }
}
