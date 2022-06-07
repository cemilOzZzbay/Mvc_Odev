using AppCore.Business.Models;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKitapciService : IService<KitapciModel, Kitapci, KitapContext> { }
    public class KitapciService : IKitapciService
    {
        public RepoBase<Kitapci, KitapContext> Repo { get; set; } = new Repo<Kitapci, KitapContext>();
        private RepoBase<KitapKitapci, KitapContext> _kitapKitapciRepo = new Repo<KitapKitapci, KitapContext>();

        public Result Add(KitapciModel model)
        {
            if (Repo.Query().Any(kitapci => kitapci.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girdiğiniz kitapcı adına sahip kayıt bulunmaktadır!");
            Kitapci entity = new Kitapci()
            {
                Adi = model.Adi.Trim(),
                SanalMi = model.SanalMi
            };
            Repo.Add(entity);
            return new SuccessResult("Kitapcı başarıyla eklendi.");
        }

        public Result Delete(int id)
        {
            Kitapci entity = Repo.Query("KitapKitapcilar").SingleOrDefault(kitapci => kitapci.Id == id);
            if (entity.KitapKitapcilar != null && entity.KitapKitapcilar.Count > 0)
            {
                foreach (var kitapKitapci in entity.KitapKitapcilar)
                {
                    _kitapKitapciRepo.Delete(kitapKitapci, false);
                }
                _kitapKitapciRepo.Save();
                _kitapKitapciRepo.Dispose();
            }
            Repo.Delete(entity);
            return new SuccessResult("Kitapcı başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KitapciModel> Query()
        {
            return Repo.Query().OrderBy(kitapci => kitapci.Adi).Select(kitapci => new KitapciModel()
            {
                Id = kitapci.Id,
                Adi = kitapci.Adi,
                SanalMi = kitapci.SanalMi,

                SanalMiDisplay = kitapci.SanalMi ? "Evet" : "Hayır"
            });
        }

        public Result Update(KitapciModel model)
        {
            if (Repo.Query().Any(kitapci => kitapci.Adi.ToLower() == model.Adi.ToLower().Trim() && kitapci.Id != model.Id))
                return new ErrorResult("Girdiğiniz kitapcı adına sahip kayıt bulunmaktadır!");
            Kitapci entity = Repo.Query(kitapci => kitapci.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();
            entity.SanalMi = model.SanalMi;
            Repo.Update(entity);
            return new SuccessResult();
        }
    }
}
