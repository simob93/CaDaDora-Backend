using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Aggiunto_Campo_TabellaPersonalizzazioni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostoTransazioneBancaria",
                table: "AppPersonalizzazioni",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataInizio",
                table: "AppBookingPrenotazione",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataFine",
                table: "AppBookingPrenotazione",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoTransazioneBancaria",
                table: "AppPersonalizzazioni");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInizio",
                table: "AppBookingPrenotazione",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFine",
                table: "AppBookingPrenotazione",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
