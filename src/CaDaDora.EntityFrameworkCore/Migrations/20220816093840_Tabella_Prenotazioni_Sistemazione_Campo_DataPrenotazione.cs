using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Tabella_Prenotazioni_Sistemazione_Campo_DataPrenotazione : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPrenotazione",
                table: "AppBookingPrenotazione",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataPrenotazione",
                table: "AppBookingPrenotazione",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
