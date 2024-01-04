using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectAccessibility.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aandoeningen",
                columns: table => new
                {
                    Acode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aandoeningen", x => x.Acode);
                });

            migrationBuilder.CreateTable(
                name: "Beperkingen",
                columns: table => new
                {
                    Bcode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beperkingen", x => x.Bcode);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    Gcode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Wachtwoord = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker", x => x.Gcode);
                });

            migrationBuilder.CreateTable(
                name: "Hulpmiddelen",
                columns: table => new
                {
                    Hcode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hulpmiddelen", x => x.Hcode);
                });

            migrationBuilder.CreateTable(
                name: "Onderzoeken",
                columns: table => new
                {
                    Ocode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titel = table.Column<string>(type: "text", nullable: false),
                    Beschrijving = table.Column<string>(type: "text", nullable: false),
                    Locatie = table.Column<string>(type: "text", nullable: false),
                    Startdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Einddatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderzoeken", x => x.Ocode);
                });

            migrationBuilder.CreateTable(
                name: "Onderzoekstypes",
                columns: table => new
                {
                    Otcode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderzoekstypes", x => x.Otcode);
                });

            migrationBuilder.CreateTable(
                name: "Voogden",
                columns: table => new
                {
                    Vcode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Voornaam = table.Column<string>(type: "text", nullable: false),
                    Achternaam = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefoonnummer = table.Column<string>(type: "text", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voogden", x => x.Vcode);
                });

            migrationBuilder.CreateTable(
                name: "Bedrijven",
                columns: table => new
                {
                    Gcode = table.Column<int>(type: "integer", nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false),
                    Locatie = table.Column<string>(type: "text", nullable: false),
                    Bedrijfsinformatie = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijven", x => x.Gcode);
                    table.ForeignKey(
                        name: "FK_Bedrijven_Gebruiker_Gcode",
                        column: x => x.Gcode,
                        principalTable: "Gebruiker",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beheerders",
                columns: table => new
                {
                    Gcode = table.Column<int>(type: "integer", nullable: false),
                    Voornaam = table.Column<string>(type: "text", nullable: false),
                    Achternaam = table.Column<string>(type: "text", nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beheerders", x => x.Gcode);
                    table.ForeignKey(
                        name: "FK_Beheerders_Gebruiker_Gcode",
                        column: x => x.Gcode,
                        principalTable: "Gebruiker",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Ccode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderGCode = table.Column<int>(type: "integer", nullable: false),
                    RecieverGCode = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Ccode);
                    table.ForeignKey(
                        name: "FK_Chat_Gebruiker_RecieverGCode",
                        column: x => x.RecieverGCode,
                        principalTable: "Gebruiker",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_Gebruiker_SenderGCode",
                        column: x => x.SenderGCode,
                        principalTable: "Gebruiker",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ervaringdeskundigen",
                columns: table => new
                {
                    Gcode = table.Column<int>(type: "integer", nullable: false),
                    Voornaam = table.Column<string>(type: "text", nullable: false),
                    Achternaam = table.Column<string>(type: "text", nullable: false),
                    Telefoonnummer = table.Column<string>(type: "text", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: false),
                    Straatnaam = table.Column<string>(type: "text", nullable: false),
                    Huisnummer = table.Column<string>(type: "text", nullable: false),
                    Plaats = table.Column<string>(type: "text", nullable: false),
                    Contactvoorkeur = table.Column<string>(type: "text", nullable: false),
                    Commercieel = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ervaringdeskundigen", x => x.Gcode);
                    table.ForeignKey(
                        name: "FK_Ervaringdeskundigen_Gebruiker_Gcode",
                        column: x => x.Gcode,
                        principalTable: "Gebruiker",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeeftTypes",
                columns: table => new
                {
                    Otcode = table.Column<int>(type: "integer", nullable: false),
                    Ocode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeeftTypes", x => new { x.Otcode, x.Ocode });
                    table.ForeignKey(
                        name: "FK_HeeftTypes_Onderzoeken_Ocode",
                        column: x => x.Ocode,
                        principalTable: "Onderzoeken",
                        principalColumn: "Ocode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeeftTypes_Onderzoekstypes_Otcode",
                        column: x => x.Otcode,
                        principalTable: "Onderzoekstypes",
                        principalColumn: "Otcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeeftOnderzoeken",
                columns: table => new
                {
                    Ocode = table.Column<int>(type: "integer", nullable: false),
                    Bcode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeeftOnderzoeken", x => new { x.Bcode, x.Ocode });
                    table.ForeignKey(
                        name: "FK_HeeftOnderzoeken_Bedrijven_Bcode",
                        column: x => x.Bcode,
                        principalTable: "Bedrijven",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeeftOnderzoeken_Onderzoeken_Ocode",
                        column: x => x.Ocode,
                        principalTable: "Onderzoeken",
                        principalColumn: "Ocode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeeftBeperkingen",
                columns: table => new
                {
                    Bcode = table.Column<int>(type: "integer", nullable: false),
                    Ecode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeeftBeperkingen", x => new { x.Bcode, x.Ecode });
                    table.ForeignKey(
                        name: "FK_HeeftBeperkingen_Beperkingen_Bcode",
                        column: x => x.Bcode,
                        principalTable: "Beperkingen",
                        principalColumn: "Bcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeeftBeperkingen_Ervaringdeskundigen_Ecode",
                        column: x => x.Ecode,
                        principalTable: "Ervaringdeskundigen",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeeftHulpmiddelen",
                columns: table => new
                {
                    Hcode = table.Column<int>(type: "integer", nullable: false),
                    Ecode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeeftHulpmiddelen", x => new { x.Hcode, x.Ecode });
                    table.ForeignKey(
                        name: "FK_HeeftHulpmiddelen_Ervaringdeskundigen_Ecode",
                        column: x => x.Ecode,
                        principalTable: "Ervaringdeskundigen",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeeftHulpmiddelen_Hulpmiddelen_Hcode",
                        column: x => x.Hcode,
                        principalTable: "Hulpmiddelen",
                        principalColumn: "Hcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeeftVoogden",
                columns: table => new
                {
                    Vcode = table.Column<int>(type: "integer", nullable: false),
                    Ecode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeeftVoogden", x => new { x.Vcode, x.Ecode });
                    table.ForeignKey(
                        name: "FK_HeeftVoogden_Ervaringdeskundigen_Ecode",
                        column: x => x.Ecode,
                        principalTable: "Ervaringdeskundigen",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeeftVoogden_Voogden_Vcode",
                        column: x => x.Vcode,
                        principalTable: "Voogden",
                        principalColumn: "Vcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onderzoeksresultaten",
                columns: table => new
                {
                    Ocode = table.Column<int>(type: "integer", nullable: false),
                    Ecode = table.Column<int>(type: "integer", nullable: false),
                    Orcode = table.Column<int>(type: "integer", nullable: false),
                    Datum = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Antwoord = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderzoeksresultaten", x => new { x.Ocode, x.Ecode });
                    table.ForeignKey(
                        name: "FK_Onderzoeksresultaten_Ervaringdeskundigen_Ecode",
                        column: x => x.Ecode,
                        principalTable: "Ervaringdeskundigen",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onderzoeksresultaten_Onderzoeken_Ocode",
                        column: x => x.Ocode,
                        principalTable: "Onderzoeken",
                        principalColumn: "Ocode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoorkeurTypes",
                columns: table => new
                {
                    Otcode = table.Column<int>(type: "integer", nullable: false),
                    Ecode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoorkeurTypes", x => new { x.Otcode, x.Ecode });
                    table.ForeignKey(
                        name: "FK_VoorkeurTypes_Ervaringdeskundigen_Ecode",
                        column: x => x.Ecode,
                        principalTable: "Ervaringdeskundigen",
                        principalColumn: "Gcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoorkeurTypes_Onderzoekstypes_Otcode",
                        column: x => x.Otcode,
                        principalTable: "Onderzoekstypes",
                        principalColumn: "Otcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_RecieverGCode",
                table: "Chat",
                column: "RecieverGCode");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_SenderGCode",
                table: "Chat",
                column: "SenderGCode");

            migrationBuilder.CreateIndex(
                name: "IX_HeeftBeperkingen_Ecode",
                table: "HeeftBeperkingen",
                column: "Ecode");

            migrationBuilder.CreateIndex(
                name: "IX_HeeftHulpmiddelen_Ecode",
                table: "HeeftHulpmiddelen",
                column: "Ecode");

            migrationBuilder.CreateIndex(
                name: "IX_HeeftOnderzoeken_Ocode",
                table: "HeeftOnderzoeken",
                column: "Ocode");

            migrationBuilder.CreateIndex(
                name: "IX_HeeftTypes_Ocode",
                table: "HeeftTypes",
                column: "Ocode");

            migrationBuilder.CreateIndex(
                name: "IX_HeeftVoogden_Ecode",
                table: "HeeftVoogden",
                column: "Ecode");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoeksresultaten_Ecode",
                table: "Onderzoeksresultaten",
                column: "Ecode");

            migrationBuilder.CreateIndex(
                name: "IX_VoorkeurTypes_Ecode",
                table: "VoorkeurTypes",
                column: "Ecode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aandoeningen");

            migrationBuilder.DropTable(
                name: "Beheerders");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "HeeftBeperkingen");

            migrationBuilder.DropTable(
                name: "HeeftHulpmiddelen");

            migrationBuilder.DropTable(
                name: "HeeftOnderzoeken");

            migrationBuilder.DropTable(
                name: "HeeftTypes");

            migrationBuilder.DropTable(
                name: "HeeftVoogden");

            migrationBuilder.DropTable(
                name: "Onderzoeksresultaten");

            migrationBuilder.DropTable(
                name: "VoorkeurTypes");

            migrationBuilder.DropTable(
                name: "Beperkingen");

            migrationBuilder.DropTable(
                name: "Hulpmiddelen");

            migrationBuilder.DropTable(
                name: "Bedrijven");

            migrationBuilder.DropTable(
                name: "Voogden");

            migrationBuilder.DropTable(
                name: "Onderzoeken");

            migrationBuilder.DropTable(
                name: "Ervaringdeskundigen");

            migrationBuilder.DropTable(
                name: "Onderzoekstypes");

            migrationBuilder.DropTable(
                name: "Gebruiker");
        }
    }
}
