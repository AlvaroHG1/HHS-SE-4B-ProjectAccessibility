using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAccessibility.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Bedrijven");

            migrationBuilder.AddColumn<string>(
                name: "GezochteBeperking",
                table: "Onderzoeken",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GezochtePostcode",
                table: "Onderzoeken",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxLeeftijd",
                table: "Onderzoeken",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinLeeftijd",
                table: "Onderzoeken",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Geboortedatum",
                table: "Ervaringdeskundigen",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GezochteBeperking",
                table: "Onderzoeken");

            migrationBuilder.DropColumn(
                name: "GezochtePostcode",
                table: "Onderzoeken");

            migrationBuilder.DropColumn(
                name: "MaxLeeftijd",
                table: "Onderzoeken");

            migrationBuilder.DropColumn(
                name: "MinLeeftijd",
                table: "Onderzoeken");

            migrationBuilder.DropColumn(
                name: "Geboortedatum",
                table: "Ervaringdeskundigen");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Bedrijven",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
