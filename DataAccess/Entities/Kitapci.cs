using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Kitapci : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Adi { get; set; }

        public bool SanalMi { get; set; }

        public List<KitapKitapci> KitapKitapcilar { get; set; }
    }
}
