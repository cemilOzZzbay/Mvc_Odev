using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace Mvc_Odev.Controllers
{
    //public void ObsoleteTest()
    //{
    //    Seed();
    //}

    //[NonAction] // aksiyonun web uygulaması üzerinden çağırılmasını engeller
    //[Obsolete("Bu method artık kullanılmamaktadır!")]
    public class DatabaseController : Controller
    {
        public IActionResult Seed()
        {
            using (KitapContext db = new KitapContext())
            {
                var kitapKitapciEntity = db.KitapKitapcilar.ToList();
                db.KitapKitapcilar.RemoveRange(kitapKitapciEntity);

                var kitapcilarEntity = db.Kitapcilar.ToList();
                db.Kitapcilar.RemoveRange(kitapcilarEntity);

                var kitaplarEntity = db.Kitaplar.ToList();
                db.Kitaplar.RemoveRange(kitaplarEntity);

                var turlerEntity = db.Turler.ToList();
                db.Turler.RemoveRange(turlerEntity);

                var kullaniciDetayiEntities = db.KullaniciDetaylari.ToList();
                db.KullaniciDetaylari.RemoveRange(kullaniciDetayiEntities);

                var kullaniciEntities = db.Kullanicilar.ToList();
                db.Kullanicilar.RemoveRange(kullaniciEntities);

                var rolEntities = db.Roller.ToList();
                db.Roller.RemoveRange(rolEntities);

                var sehirEntities = db.Sehirler.ToList();
                db.Sehirler.RemoveRange(sehirEntities);

                var ulkeEntities = db.Ulkeler.ToList();
                db.Ulkeler.RemoveRange(ulkeEntities);


                if (turlerEntity.Count > 0)
                {
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kitaplar', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Turler', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kitapcilar', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kullanicilar', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Roller', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Sehirler', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Ulkeler', reseed, 0)");
                    db.Database.ExecuteSqlRaw("dbcc checkident ('Kitapcilar', reseed, 0)");
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
               
                db.Ulkeler.Add(new Ulke()
                {
                    Adi = "Türkiye",
                    Sehirler = new List<Sehir>()
                    {
                        new Sehir()
                        {
                            Adi = "Ankara"
                        },
                        new Sehir()
                        {
                            Adi = "İstanbul"
                        },
                        new Sehir()
                        {
                            Adi = "İzmir"
                        }
                    }
                });
                db.Ulkeler.Add(new Ulke()
                {
                    Adi = "Amerika Birleşik Devletleri",
                    Sehirler = new List<Sehir>()
                    {
                        new Sehir()
                        {
                            Adi = "New York"
                        },
                        new Sehir()
                        {
                            Adi = "Los Angeles"
                        }
                    }
                });

                db.SaveChanges();

                db.Kitapcilar.Add(new Kitapci()
                {
                    Adi = "A Kitabevi",
                    SanalMi = true,
                    KitapKitapcilar = new List<KitapKitapci>()
                    {
                        new KitapKitapci()
                        {
                            KitapId = db.Kitaplar.SingleOrDefault(kitap => kitap.Adi == "BBB").Id
                        }
                    }
                });
                db.SaveChanges();

                db.Kitapcilar.Add(new Kitapci()
                {
                    Adi = "B Kitabevi",
                    SanalMi = false,
                    KitapKitapcilar = new List<KitapKitapci>()
                    {
                        new KitapKitapci()
                        {
                            KitapId = db.Kitaplar.SingleOrDefault(kitap => kitap.Adi == "AAA").Id
                        }
                    }
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
                            AktifMi = true,
                            
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Denizin ortası",
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "guvercin1@hotmail.com",
                                UlkeId = db.Ulkeler.SingleOrDefault(ulke => ulke.Adi == "Türkiye").Id,
                                SehirId = db.Sehirler.SingleOrDefault(sehir => sehir.Adi == "Ankara").Id
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
                            AktifMi = true,
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Dağın başı",
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "guvercin2@hotmail.com",
                                UlkeId = db.Ulkeler.SingleOrDefault(ulke => ulke.Adi == "Amerika Birleşik Devletleri").Id,
                                SehirId = db.Sehirler.SingleOrDefault(sehir => sehir.Adi == "Los Angeles").Id
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
