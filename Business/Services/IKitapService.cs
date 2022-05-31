using AppCore.Business.Models;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface IKitapService : IService<KitapModel, Kitap, KitapContext> { }

    public class KitapService : IKitapService
    {
        public RepoBase<Kitap, KitapContext> Repo { get; set; } = new Repo<Kitap, KitapContext>();

        public Result Add(KitapModel model)
        {
            if (Repo.Query().Any(kitap => kitap.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girdiğiniz kitap adına sahip kayıt bulunmaktadır!");

            Kitap entity = new Kitap()
            {
                Adi = model.Adi.Trim(),
                Aciklamasi = model.Aciklamasi?.Trim(),
                YayımTarihi = model.YayımTarihi,
                BirimFiyati = model.BirimFiyati.Value,
                StokMiktari = model.StokMiktari.Value,
                TurId = model.TurId.Value,
            };
            Repo.Add(entity);
            return new SuccessResult("Kitap başarıyla eklendi.");
        }

        public Result Delete(int id)
        {
            Repo.Delete(kitap => kitap.Id == id);
            return new SuccessResult("Kitap başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KitapModel> Query()
        {
            return Repo.Query("Tur").OrderBy(kitap => kitap.Adi).Select(kitap => new KitapModel()
            {
                Id = kitap.Id,
                Adi = kitap.Adi,
                Aciklamasi = kitap.Aciklamasi,
                YayımTarihi = kitap.YayımTarihi,
                BirimFiyati = kitap.BirimFiyati,
                StokMiktari = kitap.StokMiktari,
                TurId = kitap.TurId,

                BirimFiyatiDisplay = kitap.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                YayımTarihiDisplay = kitap.YayımTarihi.HasValue ? kitap.YayımTarihi.Value.ToString("yyyy.MM.dd") : null,
                TurAdiDisplay = kitap.Tur.Adi
            });
        }

        public Result Update(KitapModel model)
        {
            if (Repo.Query().Any(kitap => kitap.Adi.ToLower() == model.Adi.ToLower().Trim() && kitap.Id != model.Id))
                return new ErrorResult("Girdiğiniz kitap adına sahip kayıt bulunmaktadır!");

            Kitap entity = Repo.Query(kitap => kitap.Id == model.Id).SingleOrDefault();
    
            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi?.Trim();
            entity.YayımTarihi = model.YayımTarihi;
            entity.BirimFiyati = model.BirimFiyati.Value;
            entity.StokMiktari = model.StokMiktari.Value;
            entity.TurId = model.TurId.Value;
            
            Repo.Update(entity);
            return new SuccessResult();
        }
    }
}
