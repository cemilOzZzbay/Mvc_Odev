using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KitapModel : RecordBase
    {
        // Entity'den gelen özellikler
        [Required(ErrorMessage = "{0} gereklidir!")]
        [MinLength(2, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(200, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [MinLength(20, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(5000, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Açıklaması")]
        public string Aciklamasi { get; set; }

        [DisplayName("Yayım Tarihi")]
        public DateTime? YayımTarihi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} {1} ile {2} arasında olmalıdır!")]
        [DisplayName("Birim Fiyatı")]
        public double? BirimFiyati { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [Range(0, 1000000, ErrorMessage = "{0} {1} ile {2} arasında olmalıdır!")]
        [DisplayName("Stok Miktarı")]
        public int? StokMiktari { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Kitap Türü")]
        public int? TurId { get; set; }

        // Model için
        [DisplayName("Yayım Tarihi")]
        public string YayımTarihiDisplay { get; set; }

        [DisplayName("Birim Fiyatı")]
        public string BirimFiyatiDisplay { get; set; }

        [DisplayName("Kitap Türü")]
        public string TurAdiDisplay { get; set; }

    }
}
