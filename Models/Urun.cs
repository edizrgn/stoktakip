using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Models
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string BarkodNo { get; set; }
        public string UrunAdi { get; set; }
        public string Kategori { get; set; }
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public int StokMiktari { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool AktifMi { get; set; }
        public string ResimYolu { get; set; }
        public int KullaniciID { get; set; }
    }
}
