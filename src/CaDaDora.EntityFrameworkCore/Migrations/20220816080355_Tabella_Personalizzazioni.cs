using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaDaDora.Migrations
{
    public partial class Tabella_Personalizzazioni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ImpostaDiSoggiorno",
                table: "AppBookingPrenotazione",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AppPersonalizzazioni",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImpostaDiSoggiorno = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPersonalizzazioni", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPersonalizzazioni");

            migrationBuilder.DropColumn(
                name: "ImpostaDiSoggiorno",
                table: "AppBookingPrenotazione");
        }
    }
}
