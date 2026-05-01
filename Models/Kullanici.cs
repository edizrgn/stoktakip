using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; } // Admin veya Personel
        public bool Durum { get; set; } // 1: Aktif, 0: Bloklu
    }
}
