using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SehirId",
                table: "KullaniciDetaylari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UlkeId",
                table: "KullaniciDetaylari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ulkeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(105)", maxLength: 105, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sehirler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(170)", maxLength: 170, nullable: false),
                    UlkeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sehirler_Ulkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciDetaylari_SehirId",
                table: "KullaniciDetaylari",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciDetaylari_UlkeId",
                table: "KullaniciDetaylari",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_UlkeId",
                table: "Sehirler",
                column: "UlkeId");

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciDetaylari_Sehirler_SehirId",
                table: "KullaniciDetaylari",
                column: "SehirId",
                principalTable: "Sehirler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciDetaylari_Ulkeler_UlkeId",
                table: "KullaniciDetaylari",
                column: "UlkeId",
                principalTable: "Ulkeler",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciDetaylari_Sehirler_SehirId",
                table: "KullaniciDetaylari");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciDetaylari_Ulkeler_UlkeId",
                table: "KullaniciDetaylari");

            migrationBuilder.DropTable(
                name: "Sehirler");

            migrationBuilder.DropTable(
                name: "Ulkeler");

            migrationBuilder.DropIndex(
                name: "IX_KullaniciDetaylari_SehirId",
                table: "KullaniciDetaylari");

            migrationBuilder.DropIndex(
                name: "IX_KullaniciDetaylari_UlkeId",
                table: "KullaniciDetaylari");

            migrationBuilder.DropColumn(
                name: "SehirId",
                table: "KullaniciDetaylari");

            migrationBuilder.DropColumn(
                name: "UlkeId",
                table: "KullaniciDetaylari");
        }
    }
}
