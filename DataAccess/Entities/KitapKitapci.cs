using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
   public class KitapKitapci
    {
        [Key]
        public int KitapId { get; set; }
        public Kitap Kitap { get; set; }

        [Key]
        public int KitapciId { get; set; }
        public Kitapci Kitapci { get; set; }
    }
}
