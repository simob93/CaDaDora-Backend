using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Tabella_Prenotazioni_AggiuntiCampi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostoTassaDiSoggiorno",
                table: "AppBookingPrenotazione",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPrenotazione",
                table: "AppBookingPrenotazione",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoTassaDiSoggiorno",
                table: "AppBookingPrenotazione");

            migrationBuilder.DropColumn(
                name: "DataPrenotazione",
                table: "AppBookingPrenotazione");
        }
    }
}
