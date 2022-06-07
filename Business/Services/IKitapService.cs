using AppCore.Business.Models;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using Business.Models.Filters;
using DataAccess.Contexts;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface IKitapService : IService<KitapModel, Kitap, KitapContext> 
    {
        public Result<List<KitapModel>> List(KitapFiltreModel filtre);
    }

    public class KitapService : IKitapService
    {
        private KitapContext _dbContext;
        
        public RepoBase<Kitap, KitapContext> Repo { get; set; } 
        private RepoBase<KitapKitapci, KitapContext> _kitapKitapciRepo;
        private RepoBase<Kitapci, KitapContext> _kitapciRepo;
        private RepoBase<Tur, KitapContext> _turRepo;

        public KitapService()
        {
            _dbContext = new KitapContext();
            Repo = new Repo<Kitap, KitapContext>(_dbContext);
            _kitapKitapciRepo = new Repo<KitapKitapci, KitapContext>(_dbContext);
            _kitapciRepo = new Repo<Kitapci, KitapContext>(_dbContext);
            _turRepo = new Repo<Tur, KitapContext>(_dbContext);
        }
        
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
                // 2.Yöntem
                KitapKitapcilar = model.KitapciIdleri.Select(kitapciId => new KitapKitapci()
                {
                    KitapciId = kitapciId
                }).ToList()
            };
            // 1.Yöntem
            /*if(model.KitapciIdleri != null && model.KitapciIdleri.Count > 0)
            {
                entity.KitapKitapcilar = new List<KitapKitapci>();
                foreach (var kitapciId in model.KitapciIdleri)
                {
                    entity.KitapKitapcilar.Add(new KitapKitapci()
                    {
                        KitapciId = kitapciId
                    });
                }
            }*/
            Repo.Add(entity);
            return new SuccessResult("Kitap başarıyla eklendi.");
        }

        public Result Delete(int id)
        {
            var kitap = Repo.Query("KitapKitapcilar").SingleOrDefault(kitap => kitap.Id == id);
            {
                foreach (var kitapKitapci in kitap.KitapKitapcilar)
                {
                    _kitapKitapciRepo.Delete(kitapKitapci, false);
                }
                _kitapKitapciRepo.Save();
            }
            Repo.Delete(kitap => kitap.Id == id);
            return new SuccessResult("Kitap başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KitapModel> Query()
        {
            //return Repo.Query("Tur").OrderBy(kitap => kitap.Adi).Select(kitap => new KitapModel()
            //{
            //    Id = kitap.Id,
            //    Adi = kitap.Adi,
            //    Aciklamasi = kitap.Aciklamasi,
            //    YayımTarihi = kitap.YayımTarihi,
            //    BirimFiyati = kitap.BirimFiyati,
            //    StokMiktari = kitap.StokMiktari,
            //    TurId = kitap.TurId,

            //    BirimFiyatiDisplay = kitap.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
            //    YayımTarihiDisplay = kitap.YayımTarihi.HasValue ? kitap.YayımTarihi.Value.ToString("yyyy.MM.dd") : null,
            //    TurAdiDisplay = kitap.Tur.Adi
            //});

            var kitapQuery = Repo.Query();
            var kitapKitapciQuery = _kitapKitapciRepo.Query();
            var kitapciQuery = _kitapciRepo.Query();
            var turQuery = _turRepo.Query();

            // inner join
            /*var query = from kitap in kitapQuery
                        join kitap_kitapci in kitapKitapciQuery
                        on kitap.Id equals kitap_kitapci.KitapId
                        join kitapci in kitapciQuery
                        on kitap_kitapci.KitapciId equals kitapci.Id
                        join tur in turQuery
                        on kitap.TurId equals tur.Id
                        select new KitapModel()
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
                            TurAdiDisplay = tur.Adi,
                            KitapciAdlariDisplay = kitapci.Adi
                        }; */

            // left outer join
            var query = from kitap in kitapQuery
                        join kitap_kitapci in kitapKitapciQuery
                        on kitap.Id equals kitap_kitapci.KitapId into kitap_kitapcilar
                        from subKitap_Kitapcilar in kitap_kitapcilar.DefaultIfEmpty()
                        join kitapci in kitapciQuery
                        on subKitap_Kitapcilar.KitapciId equals kitapci.Id into kitapcilar
                        from subKitapcilar in kitapcilar.DefaultIfEmpty()
                        join tur in turQuery
                        on kitap.TurId equals tur.Id into turler
                        from subTurler in turler.DefaultIfEmpty()
                        select new KitapModel()
                        {
                            Id = kitap.Id,
                            Adi = kitap.Adi,
                            Aciklamasi = kitap.Aciklamasi,
                            YayımTarihi = kitap.YayımTarihi,
                            BirimFiyati = kitap.BirimFiyati,
                            StokMiktari = kitap.StokMiktari,
                            TurId = kitap.TurId,

                            //BirimFiyatiDisplay = kitap.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                            BirimFiyatiDisplay = kitap.BirimFiyati.ToString("C2"),
                            YayımTarihiDisplay = kitap.YayımTarihi.HasValue ? kitap.YayımTarihi.Value.ToString("yyyy.MM.dd") : null,
                            TurAdiDisplay = subTurler.Adi,
                            KitapciAdlariDisplay = subKitapcilar.Adi
                        };
            return query;
        }

        public Result Update(KitapModel model)
        {
            if (Repo.Query().Any(kitap => kitap.Adi.ToLower() == model.Adi.ToLower().Trim() && kitap.Id != model.Id))
                return new ErrorResult("Girdiğiniz kitap adına sahip kayıt bulunmaktadır!");

            Kitap entity = Repo.Query(kitap => kitap.Id == model.Id).SingleOrDefault();
            if (entity.KitapKitapcilar != null && entity.KitapKitapcilar.Count > 0)
            {
                foreach (var kitapKitapci in entity.KitapKitapcilar)
                {
                    _kitapKitapciRepo.Delete(kitapKitapci, false);
                }
                _kitapKitapciRepo.Save();
                _kitapKitapciRepo.Dispose();
            }
    
            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi?.Trim();
            entity.YayımTarihi = model.YayımTarihi;
            entity.BirimFiyati = model.BirimFiyati.Value;
            entity.StokMiktari = model.StokMiktari.Value;
            entity.TurId = model.TurId.Value;
            entity.KitapKitapcilar = model.KitapciIdleri.Select(kitapciId => new KitapKitapci()
            {
                KitapciId = kitapciId
            }).ToList();

            Repo.Update(entity);
            return new SuccessResult();
        }
        
        public Result<List<KitapModel>> List(KitapFiltreModel filtre)
        {
            var query = Query();
            
            if (filtre.TurId.HasValue)
                query = query.Where(q => q.TurId == filtre.TurId.Value);
            
            if (!string.IsNullOrWhiteSpace(filtre.KitapAdi))
                query = query.Where(q => q.Adi.ToUpper().Contains(filtre.KitapAdi.ToUpper().Trim()));
            
            if (filtre.BirimFiyatiBaslangic != null)
                query = query.Where(q => q.BirimFiyati >= filtre.BirimFiyatiBaslangic.Value);
            if (filtre.BirimFiyatiBitis.HasValue)
                query = query.Where(q => q.BirimFiyati <= filtre.BirimFiyatiBitis.Value);

            if (filtre.StokMiktariBaslangic != null)
                query = query.Where(q => q.StokMiktari >= filtre.StokMiktariBaslangic.Value);
            if (filtre.StokMiktariBitis.HasValue)
                query = query.Where(q => q.StokMiktari <= filtre.StokMiktariBitis.Value);

            if (filtre.YayımTarihiBaslangic != null)
                query = query.Where(q => q.YayımTarihi >= filtre.YayımTarihiBaslangic.Value);
            if (filtre.YayımTarihiBitis.HasValue)
                query = query.Where(q => q.YayımTarihi <= filtre.YayımTarihiBitis.Value);

            var list = query.ToList();
            return new SuccessResult<List<KitapModel>>(list);
        }
    }
}
