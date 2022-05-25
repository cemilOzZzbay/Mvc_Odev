using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace Mvc_Odev.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Seed()
        {
            using (KitapContext db = new KitapContext())
            {
                var kitaplarEntity = db.Kitaplar.ToList();
                db.Kitaplar.RemoveRange(kitaplarEntity);

                var turlerEntity = db.Turler.ToList();
                db.Turler.RemoveRange(turlerEntity);

                var kitapcilarEntity = db.Kitapcilar.ToList();
                db.Kitapcilar.RemoveRange(kitapcilarEntity);

                var kullaniciDetayiEntities = db.KullaniciDetaylari.ToList();
                db.KullaniciDetaylari.RemoveRange(kullaniciDetayiEntities);

                var kullaniciEntities = db.Kullanicilar.ToList();
                db.Kullanicilar.RemoveRange(kullaniciEntities);

                var rolEntities = db.Roller.ToList();
                db.Roller.RemoveRange(rolEntities);


                if (turlerEntity.Count > 0)
                {
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kitaplar', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Turler', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kitapcilar', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kullanicilar', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Roller', reseed, 0)");
                }

                db.Turler.Add(new Tur()
                {
                    Adi = "Hikaye",
                    Kitaplar = new List<Kitap>()
                    {
                        new Kitap()
                        {
                            Adi = "AAA",
                            YayımTarihi = new DateTime(2000,2,1),
                            BirimFiyati = 9.5,
                            StokMiktari = 3
                        }
                    }
                });
                db.Turler.Add(new Tur()
                {
                    Adi = "Roman",
                    Kitaplar = new List<Kitap>()
                    {
                        new Kitap()
                        {
                            Adi = "BBB",
                            YayımTarihi = DateTime.Parse("03.04.2005", new CultureInfo("tr-TR")),
                            BirimFiyati = 15.5,
                            StokMiktari = 5
                        }
                    }
                });
                db.Kitapcilar.Add(new Kitapci()
                {
                    Adi = "A Kitabevi",
                    SanalMi = true
                });
                db.Kitapcilar.Add(new Kitapci()
                {
                    Adi = "B Kitabevi",
                    SanalMi = false
                });
                db.SaveChanges();
                
                db.Roller.Add(new Rol()
                {
                    Adi = "Admin",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            KullaniciAdi = "okişi",
                            Sifre = "okişi",
                            
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Denizin ortası",
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "guvercin1@hotmail.com"
                            }
                        }
                    }
                });
                db.Roller.Add(new Rol()
                {
                    Adi = "Kullanıcı",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            KullaniciAdi = "şukişi",
                            Sifre = "şukişi",
                            
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Dağın başı",
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "guvercin2@hotmail.com"
                            }
                        }
                    }
                });
                db.SaveChanges();
            }
            return Content("<label style=\"color:red;\"><b>İlk veriler başarıyla oluşturuldu.</b></label>", "text/html", Encoding.UTF8);
        }
    }
}
