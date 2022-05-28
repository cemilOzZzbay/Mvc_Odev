using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Odev.Controllers
{
    [Authorize]
    public class KitapcilarController : Controller
    {
        private readonly IKitapciService _kitapciService;
        private readonly IKitapService _kitapService;
        public KitapcilarController(IKitapciService kitapciService, IKitapService kitapService)
        {
            _kitapciService = kitapciService;
            _kitapService = kitapService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<KitapciModel> list = _kitapciService.Query().ToList();
            return View(list);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KitapciModel kitapci)
        {
            if (ModelState.IsValid)
            {
                var result = _kitapciService.Add(kitapci);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(kitapci);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            KitapciModel model = _kitapciService.Query().SingleOrDefault(kitapci => kitapci.Id == id);
            if (model == null)
                return View("Hata", "Kitapcı bulunamadı.");

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KitapciModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _kitapciService.Update(model);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
        
        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            KitapciModel model = _kitapciService.Query().SingleOrDefault(kitapci => kitapci.Id == id.Value);
            if (model == null)
                return View("Hata", "Kitapcı bulunamadı!");
            return View(model);
        }
        
        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (!(User.Identity.IsAuthenticated && User.IsInRole("Admin")))
                return RedirectToAction("YetkisizIslem", "Hesaplar");

            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var result = _kitapciService.Delete(id.Value);
            if (result.IsSuccessful)
                TempData["Success"] = result.Message;
            else
                TempData["Error"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
