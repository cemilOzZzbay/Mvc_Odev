using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Tur : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Adi { get; set; }
        public List<Kitap> Kitaplar { get; set; }
    }
}
