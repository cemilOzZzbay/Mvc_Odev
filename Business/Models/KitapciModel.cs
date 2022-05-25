using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KitapciModel : RecordBase
    {
        // Entity'den gelen özellikler
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        public bool SanalMi { get; set; }

        // Model için
        [DisplayName("Sanal")]
        public string SanalMiDisplay { get; set; }
    }
}
