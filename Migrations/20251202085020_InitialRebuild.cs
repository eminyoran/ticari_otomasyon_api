using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OtomasyonApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialRebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cariler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    carikodu = table.Column<string>(type: "text", nullable: false),
                    unvan = table.Column<string>(type: "text", nullable: false),
                    ticariunvan = table.Column<string>(type: "text", nullable: true),
                    vergidairesi = table.Column<string>(type: "text", nullable: true),
                    vergino = table.Column<string>(type: "text", nullable: true),
                    tcno = table.Column<string>(type: "text", nullable: true),
                    telefon = table.Column<string>(type: "text", nullable: true),
                    telefon2 = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    adres = table.Column<string>(type: "text", nullable: true),
                    ulke = table.Column<string>(type: "text", nullable: true),
                    sehir = table.Column<string>(type: "text", nullable: true),
                    ilce = table.Column<string>(type: "text", nullable: true),
                    iban = table.Column<string>(type: "text", nullable: true),
                    caritipi = table.Column<string>(type: "text", nullable: true),
                    parabirimi = table.Column<string>(type: "text", nullable: true),
                    durum = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cariler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "carihareketler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cariid = table.Column<int>(type: "integer", nullable: false),
                    tarih = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    borc = table.Column<decimal>(type: "numeric", nullable: false),
                    alacak = table.Column<decimal>(type: "numeric", nullable: false),
                    evraktipi = table.Column<string>(type: "text", nullable: true),
                    referansid = table.Column<int>(type: "integer", nullable: true),
                    kasaid = table.Column<int>(type: "integer", nullable: true),
                    bankaid = table.Column<int>(type: "integer", nullable: true),
                    doviz = table.Column<string>(type: "text", nullable: true),
                    kur = table.Column<decimal>(type: "numeric", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carihareketler", x => x.id);
                    table.ForeignKey(
                        name: "FK_carihareketler_cariler_cariid",
                        column: x => x.cariid,
                        principalTable: "cariler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carihareketler_cariid",
                table: "carihareketler",
                column: "cariid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carihareketler");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "cariler");
        }
    }
}
