using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAccessibility.Migrations
{
    /// <inheritdoc />
    public partial class t : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoorkeurTypes_Onderzoekstypes_Otcode",
                table: "VoorkeurTypes");

            migrationBuilder.CreateTable(
                name: "HeeftAandoeningen",
                columns: table => new
                {
                    Acode = table.Column<int>(type: "integer", nullable: false),
                    Ecode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeeftAandoeningen", x => new { x.Ecode, x.Acode });
                    table.ForeignKey(
                        name: "FK_HeeftAandoeningen_Aandoeningen_Acode",
                        column: x => x.Acode,
                        principalTable: "Aandoeningen",
                        principalColumn: "Acode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeeftAandoeningen_Ervaringdeskundigen_Ecode",
                        column: x => x.Ecode,
                        principalTable: "Ervaringdeskundigen",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeeftAandoeningen_Acode",
                table: "HeeftAandoeningen",
                column: "Acode");

            migrationBuilder.AddForeignKey(
                name: "FK_VoorkeurTypes_Onderzoeken_Otcode",
                table: "VoorkeurTypes",
                column: "Otcode",
                principalTable: "Onderzoeken",
                principalColumn: "Ocode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoorkeurTypes_Onderzoeken_Otcode",
                table: "VoorkeurTypes");

            migrationBuilder.DropTable(
                name: "HeeftAandoeningen");

            migrationBuilder.AddForeignKey(
                name: "FK_VoorkeurTypes_Onderzoekstypes_Otcode",
                table: "VoorkeurTypes",
                column: "Otcode",
                principalTable: "Onderzoekstypes",
                principalColumn: "Otcode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
