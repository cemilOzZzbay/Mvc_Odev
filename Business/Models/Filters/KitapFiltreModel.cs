using System.ComponentModel;

namespace Business.Models.Filters
{
    public class KitapFiltreModel
    {
        [DisplayName("Kitap Adı")]
        public string KitapAdi { get; set; }
        
        [DisplayName("Birim Fiyatı")]
        public double? BirimFiyatiBaslangic { get; set; }
        public double? BirimFiyatiBitis { get; set; }

        [DisplayName("Stok Miktarı")]
        public int? StokMiktariBaslangic { get; set; }
        public int? StokMiktariBitis { get; set; }

        [DisplayName("Yayım Tarihi")]
        public DateTime? YayımTarihiBaslangic { get; set; }
        public DateTime? YayımTarihiBitis { get; set; }

        [DisplayName("Kitap Türü")]
        public int? TurId { get; set; }

        [DisplayName("Kitapcı")]
        public List<int> KitapciIdleri { get; set; }
    }
}
