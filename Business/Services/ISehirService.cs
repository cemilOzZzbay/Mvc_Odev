using AppCore.Business.Models;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface ISehirService : IService<SehirModel, Sehir, KitapContext>
    {
        Result<List<SehirModel>> List(int ulkeId);
    }
    public class SehirService : ISehirService
    {
        public RepoBase<Sehir, KitapContext> Repo { get; set; } = new Repo<Sehir, KitapContext>();

        public Result Add(SehirModel model)
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

        public IQueryable<SehirModel> Query()
        {
            return Repo.Query("Ulke").OrderBy(sehir => sehir.Adi).Select(sehir => new SehirModel()
            {
                Id = sehir.Id,
                Adi = sehir.Adi,
                UlkeId = sehir.UlkeId,
                
                UlkeAdiDisplay = sehir.Ulke.Adi
            });
        }

        public Result Update(SehirModel model)
        {
            throw new NotImplementedException();
        }

        public Result<List<SehirModel>> List(int ulkeId)
        {
            var list = Query().Where(sehir => sehir.UlkeId == ulkeId).ToList();
            return new SuccessResult<List<SehirModel>>(list);
        }
    }
}
