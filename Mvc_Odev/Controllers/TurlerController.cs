using AppCore.Business.Models;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Odev.Controllers
{
    public class TurlerController : Controller
    {
        private readonly ITurService _turService;

        public TurlerController(ITurService turService)
        {
            _turService = turService;
        }

        public IActionResult Index()
        {
            List<TurModel> model = _turService.Query().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Adi)
        {
            TurModel model = new TurModel()
            {
                Adi = Adi
            };
            var result = _turService.Add(model);
            if (result.IsSuccessful)
                return RedirectToAction(nameof(Index));
            return View("Hata", result.Message);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            TurModel model = _turService.Query().SingleOrDefault(tur => tur.Id == id);
            if (model == null)
                return View("Hata", "Tür bulunamadı!");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TurModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _turService.Update(model);
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
            TurModel model = _turService.Query().SingleOrDefault(tur => tur.Id == id.Value);
            if (model == null)
                return View("Hata", "Tür bulunamadı!");
            return View(model);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            Result result = _turService.Delete(id.Value);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
