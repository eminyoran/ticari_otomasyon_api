using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OtomasyonApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bankalar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bankaadi = table.Column<string>(type: "text", nullable: false),
                    subeadi = table.Column<string>(type: "text", nullable: true),
                    hesapno = table.Column<string>(type: "text", nullable: true),
                    iban = table.Column<string>(type: "text", nullable: true),
                    bakiye = table.Column<decimal>(type: "numeric", nullable: true),
                    parabirimi = table.Column<string>(type: "text", nullable: true),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    durum = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankalar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "faturalar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faturano = table.Column<string>(type: "text", nullable: false),
                    faturatipi = table.Column<string>(type: "text", nullable: false),
                    cariid = table.Column<int>(type: "integer", nullable: true),
                    tarih = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    toplamtutar = table.Column<decimal>(type: "numeric", nullable: true),
                    kdvtutari = table.Column<decimal>(type: "numeric", nullable: true),
                    geneltoplam = table.Column<decimal>(type: "numeric", nullable: true),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    durum = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faturalar", x => x.id);
                    table.ForeignKey(
                        name: "FK_faturalar_cariler_cariid",
                        column: x => x.cariid,
                        principalTable: "cariler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "kasalar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kasaadi = table.Column<string>(type: "text", nullable: false),
                    kasakodu = table.Column<string>(type: "text", nullable: true),
                    bakiye = table.Column<decimal>(type: "numeric", nullable: true),
                    parabirimi = table.Column<string>(type: "text", nullable: true),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    durum = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kasalar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "urunler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    urunkodu = table.Column<string>(type: "text", nullable: false),
                    adi = table.Column<string>(type: "text", nullable: false),
                    barkod = table.Column<string>(type: "text", nullable: true),
                    birim = table.Column<string>(type: "text", nullable: true),
                    birimfiyat = table.Column<decimal>(type: "numeric", nullable: true),
                    kdvorani = table.Column<decimal>(type: "numeric", nullable: true),
                    stokmiktari = table.Column<decimal>(type: "numeric", nullable: true),
                    minstokmiktari = table.Column<decimal>(type: "numeric", nullable: true),
                    kategori = table.Column<string>(type: "text", nullable: true),
                    marka = table.Column<string>(type: "text", nullable: true),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    durum = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_urunler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "faturakalemleri",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    faturaid = table.Column<int>(type: "integer", nullable: false),
                    urunid = table.Column<int>(type: "integer", nullable: false),
                    miktar = table.Column<decimal>(type: "numeric", nullable: false),
                    birimfiyat = table.Column<decimal>(type: "numeric", nullable: false),
                    kdvorani = table.Column<decimal>(type: "numeric", nullable: true),
                    kdvtutari = table.Column<decimal>(type: "numeric", nullable: true),
                    tutar = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faturakalemleri", x => x.id);
                    table.ForeignKey(
                        name: "FK_faturakalemleri_faturalar_faturaid",
                        column: x => x.faturaid,
                        principalTable: "faturalar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_faturakalemleri_urunler_urunid",
                        column: x => x.urunid,
                        principalTable: "urunler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_faturakalemleri_faturaid",
                table: "faturakalemleri",
                column: "faturaid");

            migrationBuilder.CreateIndex(
                name: "IX_faturakalemleri_urunid",
                table: "faturakalemleri",
                column: "urunid");

            migrationBuilder.CreateIndex(
                name: "IX_faturalar_cariid",
                table: "faturalar",
                column: "cariid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankalar");

            migrationBuilder.DropTable(
                name: "faturakalemleri");

            migrationBuilder.DropTable(
                name: "kasalar");

            migrationBuilder.DropTable(
                name: "faturalar");

            migrationBuilder.DropTable(
                name: "urunler");
        }
    }
}
