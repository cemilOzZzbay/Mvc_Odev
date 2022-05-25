using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Odev.Controllers
{
    public class HesaplarController : Controller
    {
        private readonly IHesapService _hesapService;
        public HesaplarController(IHesapService hesapService)
        {
            _hesapService = hesapService;
        }
        
        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Giris(KullaniciGirisModel model)
        {
            return null;
        }
    }
}
