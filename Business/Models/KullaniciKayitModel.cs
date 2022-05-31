﻿using AppCore.Records.Bases;
using DataAccess.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciKayitModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [MinLength(3, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(10, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(10, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [Compare("Sifre", ErrorMessage = "Şifre ile şifre onay aynı olmalıdır!")]
        [DisplayName("Şifre Onay")]
        public string SifreOnay { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(50, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("E-Posta")]
        public string Eposta { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        public string Adres { get; set; }

        public Cinsiyet Cinsiyet { get; set; } // Enum:Cinsiyet (Kadin:1 Erkek:2)

        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Ülke")]
        public int? UlkeId { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Şehir")]
        public int? SehirId { get; set; }

    }
}
