using AppCore.DataAccess.Configs;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class KitapContext : DbContext
    {
        public DbSet<Tur> Turler { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kitapci> Kitapcilar { get; set; }
        public DbSet<KitapKitapci> KitapKitapcilar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<KullaniciDetayi> KullaniciDetaylari { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=DESKTOP-2SEU97G;database=KitapDB;user id=sa;password=as;multipleactiveresultsets=true;";
            if (!string.IsNullOrWhiteSpace(ConnectionConfig.ConnectionString))
                optionsBuilder.UseSqlServer(connectionString);
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kitap>()
                .HasOne(kitap => kitap.Tur)
                .WithMany(tur => tur.Kitaplar)
                .HasForeignKey(kitap => kitap.TurId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kitap>()
                .HasMany(kitap => kitap.KitapKitapcilar)
                .WithOne(kitapKitapci => kitapKitapci.Kitap)
                .HasForeignKey(kitapKitapci => kitapKitapci.KitapId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kitapci>()
                .HasMany(kitapci => kitapci.KitapKitapcilar)
                .WithOne(kitapKitapci => kitapKitapci.Kitapci)
                .HasForeignKey(kitapKitapci => kitapKitapci.KitapciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KitapKitapci>()
                .HasKey(kitapKitapci => new { kitapKitapci.KitapId, kitapKitapci.KitapciId });

            modelBuilder.Entity<Kullanici>()
                .HasOne(kullanici => kullanici.Rol)
                .WithMany(rol => rol.Kullanicilar)
                .HasForeignKey(kullanici => kullanici.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kd => kd.Kullanici)
                .WithOne(kd => kd.KullaniciDetayi)
                .HasForeignKey<KullaniciDetayi>(kd => kd.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
              .HasOne(kd => kd.Ulke)
              .WithMany(ulke => ulke.KullaniciDetaylari)
              .HasForeignKey(kd => kd.UlkeId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kd => kd.Sehir)
                .WithMany(sehir => sehir.KullaniciDetaylari)
                .HasForeignKey(kd => kd.SehirId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sehir>()
                .HasOne(sehir => sehir.Ulke)
                .WithMany(ulke => ulke.Sehirler)
                .HasForeignKey(sehir => sehir.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
