using AppCore.Business.Models;
using Business.Models;

namespace Business.Services
{
    public interface IHesapService
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel kgm);
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
            KullaniciModel kullaniciModel = _kullaniciService.Query().SingleOrDefault(km => km.KullaniciAdi == kgm.KullaniciAdi && km.Sifre == kgm.Sifre);
            if(kullaniciModel == null)
                return new ErrorResult<KullaniciModel>("Geçersiz kullanıcı adı ve şifre!");
            return new SuccessResult<KullaniciModel>(kullaniciModel);
        }
    }
}
