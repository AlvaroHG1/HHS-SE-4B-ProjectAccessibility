﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectAccessibility.Context;

#nullable disable

namespace ProjectAccessibility.Migrations
{
    [DbContext(typeof(GebruikerContext))]
    [Migration("20240111210146_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectAccessibility.Models.Aandoening", b =>
                {
                    b.Property<int>("Acode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Acode"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Acode");

                    b.ToTable("Aandoeningen");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Beperking", b =>
                {
                    b.Property<int>("Bcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Bcode"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Bcode");

                    b.ToTable("Beperkingen");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Chat", b =>
                {
                    b.Property<int>("Ccode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Ccode"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecieverGCode")
                        .HasColumnType("integer");

                    b.Property<int>("SenderGCode")
                        .HasColumnType("integer");

                    b.HasKey("Ccode");

                    b.HasIndex("RecieverGCode");

                    b.HasIndex("SenderGCode");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Gebruiker", b =>
                {
                    b.Property<int>("Gcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Gcode"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Gcode");

                    b.ToTable("Gebruiker");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftAandoening", b =>
                {
                    b.Property<int>("Ecode")
                        .HasColumnType("integer");

                    b.Property<int>("Acode")
                        .HasColumnType("integer");

                    b.HasKey("Ecode", "Acode");

                    b.HasIndex("Acode");

                    b.ToTable("HeeftAandoeningen");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftBeperking", b =>
                {
                    b.Property<int>("Bcode")
                        .HasColumnType("integer");

                    b.Property<int>("Ecode")
                        .HasColumnType("integer");

                    b.HasKey("Bcode", "Ecode");

                    b.HasIndex("Ecode");

                    b.ToTable("HeeftBeperkingen");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftHulpmiddel", b =>
                {
                    b.Property<int>("Hcode")
                        .HasColumnType("integer");

                    b.Property<int>("Ecode")
                        .HasColumnType("integer");

                    b.HasKey("Hcode", "Ecode");

                    b.HasIndex("Ecode");

                    b.ToTable("HeeftHulpmiddelen");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftOnderzoek", b =>
                {
                    b.Property<int>("Bcode")
                        .HasColumnType("integer");

                    b.Property<int>("Ocode")
                        .HasColumnType("integer");

                    b.HasKey("Bcode", "Ocode");

                    b.HasIndex("Ocode");

                    b.ToTable("HeeftOnderzoeken");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftType", b =>
                {
                    b.Property<int>("Otcode")
                        .HasColumnType("integer");

                    b.Property<int>("Ocode")
                        .HasColumnType("integer");

                    b.HasKey("Otcode", "Ocode");

                    b.HasIndex("Ocode");

                    b.ToTable("HeeftTypes");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftVoogd", b =>
                {
                    b.Property<int>("Vcode")
                        .HasColumnType("integer");

                    b.Property<int>("Ecode")
                        .HasColumnType("integer");

                    b.HasKey("Vcode", "Ecode");

                    b.HasIndex("Ecode");

                    b.ToTable("HeeftVoogden");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Hulpmiddel", b =>
                {
                    b.Property<int>("Hcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Hcode"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Hcode");

                    b.ToTable("Hulpmiddelen");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Onderzoek", b =>
                {
                    b.Property<int>("Ocode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Ocode"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("Einddatum")
                        .HasColumnType("date");

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("Startdatum")
                        .HasColumnType("date");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Ocode");

                    b.ToTable("Onderzoeken");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Onderzoeksresultaat", b =>
                {
                    b.Property<int>("Ocode")
                        .HasColumnType("integer");

                    b.Property<int>("Ecode")
                        .HasColumnType("integer");

                    b.Property<string>("Antwoord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Datum")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Orcode")
                        .HasColumnType("integer");

                    b.HasKey("Ocode", "Ecode");

                    b.HasIndex("Ecode");

                    b.ToTable("Onderzoeksresultaten");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Onderzoekstype", b =>
                {
                    b.Property<int>("Otcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Otcode"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Otcode");

                    b.ToTable("Onderzoekstypes");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.OpenChat", b =>
                {
                    b.Property<int>("OCcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OCcode"));

                    b.Property<int>("RecieverGCode")
                        .HasColumnType("integer");

                    b.Property<int>("SenderGCode")
                        .HasColumnType("integer");

                    b.HasKey("OCcode");

                    b.HasIndex("RecieverGCode");

                    b.HasIndex("SenderGCode");

                    b.ToTable("OpenChats");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Voogd", b =>
                {
                    b.Property<int>("Vcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Vcode"));

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefoonnummer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Vcode");

                    b.ToTable("Voogden");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.VoorkeurType", b =>
                {
                    b.Property<int>("Otcode")
                        .HasColumnType("integer");

                    b.Property<int>("Ecode")
                        .HasColumnType("integer");

                    b.HasKey("Otcode", "Ecode");

                    b.HasIndex("Ecode");

                    b.ToTable("VoorkeurTypes");
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Bedrijf", b =>
                {
                    b.HasBaseType("ProjectAccessibility.Models.Gebruiker");

                    b.Property<string>("Bedrijfsinformatie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Bedrijven", (string)null);
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Beheerder", b =>
                {
                    b.HasBaseType("ProjectAccessibility.Models.Gebruiker");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Beheerders", (string)null);
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Ervaringdeskundige", b =>
                {
                    b.HasBaseType("ProjectAccessibility.Models.Gebruiker");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Commercieel")
                        .HasColumnType("boolean");

                    b.Property<string>("Contactvoorkeur")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Huisnummer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Plaats")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Straatnaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefoonnummer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Ervaringdeskundigen", (string)null);
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Chat", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("RecieverGCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("SenderGCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftAandoening", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Aandoening", null)
                        .WithMany()
                        .HasForeignKey("Acode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Ervaringdeskundige", null)
                        .WithMany()
                        .HasForeignKey("Ecode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftBeperking", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Beperking", null)
                        .WithMany()
                        .HasForeignKey("Bcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Ervaringdeskundige", null)
                        .WithMany()
                        .HasForeignKey("Ecode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftHulpmiddel", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Ervaringdeskundige", null)
                        .WithMany()
                        .HasForeignKey("Ecode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Hulpmiddel", null)
                        .WithMany()
                        .HasForeignKey("Hcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftOnderzoek", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Bedrijf", null)
                        .WithMany()
                        .HasForeignKey("Bcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Onderzoek", null)
                        .WithMany()
                        .HasForeignKey("Ocode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftType", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Onderzoek", null)
                        .WithMany()
                        .HasForeignKey("Ocode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Onderzoekstype", null)
                        .WithMany()
                        .HasForeignKey("Otcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.HeeftVoogd", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Ervaringdeskundige", null)
                        .WithMany()
                        .HasForeignKey("Ecode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Voogd", null)
                        .WithMany()
                        .HasForeignKey("Vcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Onderzoeksresultaat", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Ervaringdeskundige", null)
                        .WithMany()
                        .HasForeignKey("Ecode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Onderzoek", null)
                        .WithMany()
                        .HasForeignKey("Ocode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.OpenChat", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("RecieverGCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("SenderGCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.VoorkeurType", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Ervaringdeskundige", null)
                        .WithMany()
                        .HasForeignKey("Ecode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectAccessibility.Models.Onderzoek", null)
                        .WithMany()
                        .HasForeignKey("Otcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Bedrijf", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("ProjectAccessibility.Models.Bedrijf", "Gcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Beheerder", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("ProjectAccessibility.Models.Beheerder", "Gcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAccessibility.Models.Ervaringdeskundige", b =>
                {
                    b.HasOne("ProjectAccessibility.Models.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("ProjectAccessibility.Models.Ervaringdeskundige", "Gcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
