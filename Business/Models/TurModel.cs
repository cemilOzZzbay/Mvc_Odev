using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class TurModel : RecordBase
    {
        // Entity'den gelen özellikler
        [Required(ErrorMessage ="{0} gereklidir!")]
        [StringLength(100, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        // Model için 
        [DisplayName("Kitap Sayısı")]
        public int KitapSayisiDisplay { get; set; }
    }
}
