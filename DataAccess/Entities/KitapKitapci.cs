using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
   public class KitapKitapci
    {
        [Key]
        [Column(Order = 0)]
        public int KitapId { get; set; }
        public Kitap Kitap { get; set; }

        [Key]
        [Column(Order = 1)]
        public int KitapciId { get; set; }
        public Kitapci Kitapci { get; set; }
    }
}
