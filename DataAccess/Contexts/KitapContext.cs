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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-2SEU97G;database=KitapDB;user id=sa;password=as;multipleactiveresultsets=true;");
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
        }
    }
}
