using AppCore.Business.Models;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKullaniciService : IService<KullaniciModel, Kullanici, KitapContext> { }
    
    public class KullaniciService : IKullaniciService
    {
        public RepoBase<Kullanici, KitapContext> Repo { get; set; } = new Repo<Kullanici, KitapContext>();

        public Result Add(KullaniciModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KullaniciModel> Query()
        {
            return Repo.Query("Rol").Select(kullaniciEntity => new KullaniciModel() 
            {
               Id = kullaniciEntity.Id,
               KullaniciAdi = kullaniciEntity.KullaniciAdi,
               RolId = kullaniciEntity.RolId,
               Sifre = kullaniciEntity.Sifre,

               RolAdiDisplay = kullaniciEntity.Rol.Adi
            });
        }

        public Result Update(KullaniciModel model)
        {
            throw new NotImplementedException();
        }
    }
}
