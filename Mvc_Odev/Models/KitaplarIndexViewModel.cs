using Business.Models;
using Business.Models.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mvc_Odev.Models
{
    public class KitaplarIndexViewModel
    {
        public List<KitapModel> Kitaplar { get; set; }
        public KitapFiltreModel Filtre { get; set; }
        public SelectList Turler { get; set; }
    }
}
