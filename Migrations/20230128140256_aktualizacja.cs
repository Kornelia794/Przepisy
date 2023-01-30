using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zpnet.Migrations
{
    public partial class aktualizacja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autorzy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_u = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autorzy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ksiazki",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataStworzenia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    autorid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ksiazki", x => x.id);
                    table.ForeignKey(
                        name: "FK_ksiazki_autorzy_autorid",
                        column: x => x.autorid,
                        principalTable: "autorzy",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "przepisy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    przepisText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    autorid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przepisy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_przepisy_autorzy_autorid",
                        column: x => x.autorid,
                        principalTable: "autorzy",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ksiazkaprzepis",
                columns: table => new
                {
                    ksiazkiid = table.Column<int>(type: "int", nullable: false),
                    przepisyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ksiazkaprzepis", x => new { x.ksiazkiid, x.przepisyId });
                    table.ForeignKey(
                        name: "FK_ksiazkaprzepis_ksiazki_ksiazkiid",
                        column: x => x.ksiazkiid,
                        principalTable: "ksiazki",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ksiazkaprzepis_przepisy_przepisyId",
                        column: x => x.przepisyId,
                        principalTable: "przepisy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ksiazkaprzepis_przepisyId",
                table: "ksiazkaprzepis",
                column: "przepisyId");

            migrationBuilder.CreateIndex(
                name: "IX_ksiazki_autorid",
                table: "ksiazki",
                column: "autorid");

            migrationBuilder.CreateIndex(
                name: "IX_przepisy_autorid",
                table: "przepisy",
                column: "autorid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ksiazkaprzepis");

            migrationBuilder.DropTable(
                name: "ksiazki");

            migrationBuilder.DropTable(
                name: "przepisy");

            migrationBuilder.DropTable(
                name: "autorzy");
        }
    }
}
