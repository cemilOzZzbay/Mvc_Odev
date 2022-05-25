using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Kitap : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Adi { get; set; }

        [StringLength(5000)]
        public string Aciklamasi { get; set; }

        public DateTime? YayımTarihi { get; set; }

        public double BirimFiyati { get; set; }

        public int StokMiktari { get; set; }

        public int TurId { get; set; }
        public Tur Tur { get; set; }
        public List<KitapKitapci> KitapKitapcilar { get; set; }
    }
}
