using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mvc_Odev.Controllers
{
    public class KitaplarController : Controller
    {
        private readonly IKitapService _kitapService;
        private readonly ITurService _turService;

        public KitaplarController(IKitapService kitapService, ITurService turService)
        {
            _kitapService = kitapService;
            _turService = turService;
        }

        public IActionResult Index()
        {
            var model = _kitapService.Query().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["TurId"] = new SelectList(_turService.Query().ToList(), "Id", "Adi");

            KitapModel model = new KitapModel()
            {
                BirimFiyati = 0,
                StokMiktari = 0
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KitapModel kitap)
        {
            if (ModelState.IsValid)
            {
                var result = _kitapService.Add(kitap);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewData["TurId"] = new SelectList(_turService.Query().ToList(), "Id", "Adi", kitap.TurId);
            return View(kitap);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            KitapModel kitap = _kitapService.Query().SingleOrDefault(kitap => kitap.Id == id);
            if (kitap == null)
                return View("Hata", "Kitap bulunamadı!");
            ViewBag.TurId = new SelectList(_turService.Query().ToList(), "Id", "Adi", kitap.TurId);
            return View(kitap);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KitapModel kitap)
        {
            if (ModelState.IsValid)
            {
                var result = _kitapService.Update(kitap);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            ViewBag.TurId = new SelectList(_turService.Query().ToList(), "Id", "Adi", kitap.TurId.Value);
            return View(kitap);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata");
            }
            KitapModel kitap = _kitapService.Query().SingleOrDefault(kitap => kitap.Id == id.Value);
            if (kitap == null)
            {
                return View("Hata", "Kitap bulunamadı!");
            }
            return View(kitap);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var result = _kitapService.Delete(id.Value);
            if (result.IsSuccessful)
                TempData["Success"] = result.Message;
            else
                TempData["Error"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
