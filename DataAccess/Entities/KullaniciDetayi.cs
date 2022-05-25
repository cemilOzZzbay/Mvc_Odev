using DataAccess.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class KullaniciDetayi
    {
        [Key]
        public int KullaniciId { get; set; }
        
        public Kullanici Kullanici { get; set; }

        [Required]
        [StringLength(50)]
        public string Eposta { get; set; }

        [Required]
        [MinLength(30)]
        [MaxLength(150)]
        public string Adres { get; set; }

        public Cinsiyet? Cinsiyet { get; set; }
    }
}
