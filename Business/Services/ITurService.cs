using AppCore.Business.Models;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface ITurService : IService<TurModel, Tur, KitapContext> { }

    public class TurService : ITurService
    {
        public RepoBase<Tur, KitapContext> Repo { get; set; } = new Repo<Tur, KitapContext>();

        public Result Add(TurModel model)
        {
            if (Repo.Query().Any(tur => tur.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girdiğiniz kitap türünde kayıt bulunmaktadır!");

            Tur entity = new Tur()
            {
                Adi = model.Adi.Trim()
                
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Tur entity = Repo.Query("Kitaplar").SingleOrDefault(tur => tur.Id == id);
            if (entity.Kitaplar != null && entity.Kitaplar.Count > 0)
                return new ErrorResult("Kitap türü silinemez ilişkili kitap kayıtları var!");
            Repo.Delete(entity);
            return new SuccessResult("Kitap türü başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<TurModel> Query()
        {
            IQueryable<TurModel> query = Repo.Query("Kitaplar").OrderBy(tur => tur.Adi).Select(tur => new TurModel()
            {
                Id = tur.Id,
                Adi = tur.Adi,
                KitapSayisiDisplay = tur.Kitaplar.Count
            });
            return query;
        }

        public Result Update(TurModel model)
        {
            if (Repo.Query().Any(tur => tur.Adi.ToLower() == model.Adi.ToLower().Trim() && tur.Id != model.Id))
                return new ErrorResult("Girdiğiniz kitap türünde kayıt bulunmaktadır!");
            Tur entity = Repo.Query().SingleOrDefault(tur => tur.Id == model.Id);
            entity.Adi = model.Adi.Trim();
            Repo.Update(entity);
            return new SuccessResult("Kitap türü başarıyla güncellendi.");
        }
    }
}
