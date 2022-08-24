using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Sistemazione_Campi_BookingPrenotazione : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrenotazioneTeId",
                table: "AppBookingPrenotazione");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrenotazioneTeId",
                table: "AppBookingPrenotazione",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
