using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Aggiunto_Campi_Tabella_Prenotazioni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostoTransazione",
                table: "AppBookingPrenotazione",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentualeTransazione",
                table: "AppBookingPrenotazione",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoTransazione",
                table: "AppBookingPrenotazione");

            migrationBuilder.DropColumn(
                name: "PercentualeTransazione",
                table: "AppBookingPrenotazione");
        }
    }
}
