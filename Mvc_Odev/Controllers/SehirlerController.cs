using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Odev.Controllers
{
    public class SehirlerController : Controller
    {
        private readonly ISehirService _sehirService;

        public SehirlerController(ISehirService sehirService)
        {
            _sehirService = sehirService;
        }

        [HttpGet]
        public IActionResult SehirlerGet(int ulkeId) 
        {
            //var model = _sehirService.Query().ToList();
            var result = _sehirService.List(ulkeId);
            var model = result.Data;
            return Json(model);

        }
        [HttpPost]
        public IActionResult SehirlerPost(int ulkeId)
        {
            //var model = _sehirService.Query().ToList();
            var result = _sehirService.List(ulkeId);
            var model = result.Data;
            return Json(model);
        }
    }
}
