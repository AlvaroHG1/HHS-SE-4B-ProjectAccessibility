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

            modelBuilder.Entity<Chat>()
                .HasKey(c => c.Ccode);
            
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

            modelBuilder.Entity<OpenChat>()
                .HasKey(oc => oc.OCcode);
            
            modelBuilder.Entity<HeeftVoogd>()
                .HasKey(hv => new { hv.Vcode, hv.Ecode });
            
            modelBuilder.Entity<HeeftType>()
                .HasKey(ht => new { ht.Otcode, ht.Ocode });
            
            modelBuilder.Entity<HeeftAandoening>()
                .HasKey(ha => new { ha.Ecode, ha.Acode });
            
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
            
            modelBuilder.Entity<HeeftVoogd>()
                .HasOne<Voogd>()
                .WithMany()
                .HasForeignKey(hv => hv.Vcode);

            modelBuilder.Entity<HeeftVoogd>()
                .HasOne<Ervaringdeskundige>()
                .WithMany()
                .HasForeignKey(hv => hv.Ecode);
            
            modelBuilder.Entity<HeeftAandoening>()
                .HasOne<Aandoening>()
                .WithMany()
                .HasForeignKey(ha => ha.Acode);

            modelBuilder.Entity<HeeftAandoening>()
                .HasOne<Ervaringdeskundige>()
                .WithMany()
                .HasForeignKey(ha => ha.Ecode);
            
            modelBuilder.Entity<HeeftOnderzoek>()
                .HasOne<Onderzoek>()
                .WithMany()
                .HasForeignKey(ho => ho.Ocode);

            modelBuilder.Entity<HeeftOnderzoek>()
                .HasOne<Bedrijf>()
                .WithMany()
                .HasForeignKey(ho => ho.Bcode);
            
            modelBuilder.Entity<Onderzoeksresultaat>()
                .HasOne<Onderzoek>()
                .WithMany()
                .HasForeignKey(or => or.Ocode);

            modelBuilder.Entity<Onderzoeksresultaat>()
                .HasOne<Ervaringdeskundige>()
                .WithMany()
                .HasForeignKey(or => or.Ecode);
            
            modelBuilder.Entity<HeeftType>()
                .HasOne<Onderzoekstype>()
                .WithMany()
                .HasForeignKey(ht => ht.Otcode);

            modelBuilder.Entity<HeeftType>()
                .HasOne<Onderzoek>()
                .WithMany()
                .HasForeignKey(ht => ht.Ocode);
            
            modelBuilder.Entity<VoorkeurType>()
                .HasOne<Onderzoek>()
                .WithMany()
                .HasForeignKey(vt => vt.Otcode);

            modelBuilder.Entity<VoorkeurType>()
                .HasOne<Ervaringdeskundige>()
                .WithMany()
                .HasForeignKey(vt => vt.Ecode);
            
            modelBuilder.Entity<HeeftBeperking>()
                .HasOne<Beperking>()
                .WithMany()
                .HasForeignKey(hb => hb.Bcode);

            modelBuilder.Entity<HeeftBeperking>()
                .HasOne<Ervaringdeskundige>()
                .WithMany()
                .HasForeignKey(hb => hb.Ecode);
            
            modelBuilder.Entity<HeeftHulpmiddel>()
                .HasOne<Hulpmiddel>()
                .WithMany()
                .HasForeignKey(hh => hh.Hcode);

            modelBuilder.Entity<HeeftHulpmiddel>()
                .HasOne<Ervaringdeskundige>()
                .WithMany()
                .HasForeignKey(hh => hh.Ecode);

            modelBuilder.Entity<Chat>()
                .HasOne<Gebruiker>()
                .WithMany()
                .HasForeignKey(c => c.SenderGCode);

            modelBuilder.Entity<Chat>()
                .HasOne<Gebruiker>()
                .WithMany()
                .HasForeignKey(c => c.RecieverGCode);
            
            modelBuilder.Entity<OpenChat>()
                .HasOne<Gebruiker>()
                .WithMany()
                .HasForeignKey(oc => oc.SenderGCode);

            modelBuilder.Entity<OpenChat>()
                .HasOne<Gebruiker>()
                .WithMany()
                .HasForeignKey(oc => oc.RecieverGCode);
            
            modelBuilder.Entity<Ervaringdeskundige>()
                .ToTable("Ervaringdeskundigen");

            modelBuilder.Entity<Bedrijf>()
                .ToTable("Bedrijven");

            modelBuilder.Entity<Beheerder>()
                .ToTable("Beheerders"); 

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Beheerder> Beheerders { get; set; }
        public DbSet<Ervaringdeskundige> Ervaringdeskundiges { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
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
        public DbSet<HeeftAandoening> HeeftAandoeningen { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<OpenChat> OpenChats { get; set; }
    }
}