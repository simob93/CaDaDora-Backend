using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Sistemazione_Tabelle_BookingPrenotazione : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBookingPrenotazione",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrenotazioneTeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrenotazioneBookingId = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Cognome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DataInizio = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataFine = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NumeroPersone = table.Column<int>(type: "integer", nullable: false),
                    NumeroAdulti = table.Column<int>(type: "integer", nullable: true),
                    NumeroBambini = table.Column<int>(type: "integer", nullable: true),
                    EtaBambini = table.Column<string>(type: "text", nullable: true),
                    CostoAppartamento = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoCommissione = table.Column<decimal>(type: "numeric", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBookingPrenotazione", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBookingPrenotazione");
        }
    }
}
