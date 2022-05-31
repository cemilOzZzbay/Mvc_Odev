using AppCore.Business.Models;
using Business.Enums;
using Business.Models;

namespace Business.Services
{
    public interface IHesapService
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel kgm);
        Result Kayit(KullaniciKayitModel model);
    }
    public class HesapService : IHesapService
    {
        private readonly IKullaniciService _kullaniciService;
        public HesapService(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        
        public Result<KullaniciModel> Giris(KullaniciGirisModel kgm)
        {
            KullaniciModel kullaniciModel = _kullaniciService.Query().SingleOrDefault(km => km.KullaniciAdi == kgm.KullaniciAdi && km.Sifre == kgm.Sifre && km.AktifMi == true);
            if(kullaniciModel == null)
                return new ErrorResult<KullaniciModel>("Geçersiz kullanıcı adı ve şifre!");
            return new SuccessResult<KullaniciModel>(kullaniciModel);
        }

        public Result Kayit(KullaniciKayitModel model)
        {
            KullaniciModel kullanici = new KullaniciModel()
            {
                AktifMi = true,
                KullaniciAdi = model.KullaniciAdi,
                RolId = (int)Roller.Kullanıcı,
                Sifre = model.Sifre,
                Adres = model.Adres.Trim(),
                Eposta = model.Eposta,
                UlkeId = model.UlkeId,
                SehirId = model.SehirId,
                Cinsiyet = model.Cinsiyet
            };
            return _kullaniciService.Add(kullanici);
        }
    }
}
