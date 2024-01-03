using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Context
{
    public class GebruikerContext : DbContext
    {
        public GebruikerContext(DbContextOptions<GebruikerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>()
                .HasKey(g => g.Gcode);
            
            modelBuilder.Entity<Aandoening>()
                .HasKey(a => a.Acode);
            
            modelBuilder.Entity<Beperking>()
                .HasKey(b => b.Bcode);
            
            modelBuilder.Entity<Onderzoek>()
                .HasKey(o => o.Ocode);
            
            modelBuilder.Entity<Hulpmiddel>()
                .HasKey(h => h.Hcode);

            modelBuilder.Entity<Onderzoekstype>()
                .HasKey(o => o.Otcode);
            
            modelBuilder.Entity<Voogd>()
                .HasKey(v => v.Vcode);
            
            modelBuilder.Entity<HeeftVoogd>()
                .HasKey(hv => new { hv.Vcode, hv.Ecode });
            
            modelBuilder.Entity<HeeftType>()
                .HasKey(ht => new { ht.Otcode, ht.Ocode });
            
            modelBuilder.Entity<VoorkeurType>()
                .HasKey(vt => new { vt.Otcode, vt.Ecode });
            
            modelBuilder.Entity<HeeftBeperking>()
                .HasKey(hb => new { hb.Bcode, hb.Ecode });
            
            modelBuilder.Entity<HeeftHulpmiddel>()
                .HasKey(hh => new { hh.Hcode, hh.Ecode });
            
            modelBuilder.Entity<Onderzoeksresultaat>()
                .HasKey(o => new { o.Ocode, o.Ecode });
            
            modelBuilder.Entity<HeeftOnderzoek>()
                .HasKey(ho => new { ho.Bcode, ho.Ocode });

            modelBuilder.Entity<Ervaringdeskundige>()
                .HasOne(e => e.Gebruiker)
                .WithOne()
                .HasForeignKey<Ervaringdeskundige>(e => e.Gcode);

            modelBuilder.Entity<Bedrijf>()
                .HasOne(b => b.Gebruiker)
                .WithOne()
                .HasForeignKey<Bedrijf>(b => b.Gcode);

            modelBuilder.Entity<Beheerder>()
                .HasOne(b => b.Gebruiker)
                .WithOne()
                .HasForeignKey<Beheerder>(b => b.Gcode);
            
            modelBuilder.Entity<HeeftVoogd>()
                .HasOne(hv => hv.Voogd)
                .WithMany()
                .HasForeignKey(hv => hv.Vcode);

            modelBuilder.Entity<HeeftVoogd>()
                .HasOne(hv => hv.Ervaringdeskundige)
                .WithMany()
                .HasForeignKey(hv => hv.Ecode);
            
            modelBuilder.Entity<HeeftOnderzoek>()
                .HasOne(ho => ho.Onderzoek)
                .WithMany()
                .HasForeignKey(ho => ho.Ocode);

            modelBuilder.Entity<HeeftOnderzoek>()
                .HasOne(ho => ho.Bedrijf)
                .WithMany()
                .HasForeignKey(ho => ho.Bcode);
            
            modelBuilder.Entity<Onderzoeksresultaat>()
                .HasOne(or => or.Onderzoek)
                .WithMany()
                .HasForeignKey(or => or.Ocode);

            modelBuilder.Entity<Onderzoeksresultaat>()
                .HasOne(or => or.Ervaringdeskundige)
                .WithMany()
                .HasForeignKey(or => or.Ecode);
            
            modelBuilder.Entity<HeeftType>()
                .HasOne(ht => ht.Onderzoekstype)
                .WithMany()
                .HasForeignKey(ht => ht.Otcode);

            modelBuilder.Entity<HeeftType>()
                .HasOne(ht => ht.Onderzoek)
                .WithMany()
                .HasForeignKey(ht => ht.Ocode);
            
            modelBuilder.Entity<VoorkeurType>()
                .HasOne(vt => vt.Onderzoekstype)
                .WithMany()
                .HasForeignKey(vt => vt.Otcode);

            modelBuilder.Entity<VoorkeurType>()
                .HasOne(vt => vt.Ervaringdeskundige)
                .WithMany()
                .HasForeignKey(vt => vt.Ecode);
            
            modelBuilder.Entity<HeeftBeperking>()
                .HasOne(hb => hb.Beperking)
                .WithMany()
                .HasForeignKey(hb => hb.Bcode);

            modelBuilder.Entity<HeeftBeperking>()
                .HasOne(hb => hb.Ervaringdeskundige)
                .WithMany()
                .HasForeignKey(hb => hb.Ecode);
            
            modelBuilder.Entity<HeeftHulpmiddel>()
                .HasOne(hh => hh.Hulpmiddel)
                .WithMany()
                .HasForeignKey(hh => hh.Hcode);

            modelBuilder.Entity<HeeftHulpmiddel>()
                .HasOne(hh => hh.Ervaringdeskundige)
                .WithMany()
                .HasForeignKey(hh => hh.Ecode);
            
            modelBuilder.Entity<Ervaringdeskundige>()
                .ToTable("ervaringdeskundigen");

            modelBuilder.Entity<Bedrijf>()
                .ToTable("bedrijven");

            modelBuilder.Entity<Beheerder>()
                .ToTable("beheerders");


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Aandoening> Aandoeningen { get; set; }
        public DbSet<Beperking> Beperkingen { get; set; }
        public DbSet<Hulpmiddel> Hulpmiddelen { get; set; }
        public DbSet<Voogd> Voogden { get; set; }
        public DbSet<HeeftVoogd> HeeftVoogden { get; set; }
        public DbSet<Onderzoekstype> Onderzoekstypes { get; set; }
        public DbSet<Onderzoek> Onderzoeken { get; set; }
        public DbSet<HeeftOnderzoek> HeeftOnderzoeken { get; set; }
        public DbSet<Onderzoeksresultaat> Onderzoeksresultaten { get; set; }
        public DbSet<HeeftBeperking> HeeftBeperkingen { get; set; }
        public DbSet<HeeftType> HeeftTypes { get; set; }
        public DbSet<VoorkeurType> VoorkeurTypes { get; set; }
        public DbSet<HeeftHulpmiddel> HeeftHulpmiddelen { get; set; }
    }
}