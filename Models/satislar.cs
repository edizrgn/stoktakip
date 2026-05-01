using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Models
{
    public class satislar
    {
        // 1. Birincil Anahtar (Primary Key)
        // Veritabanındaki 'Id' sütunuyla birebir eşleşir.
        public int Id { get; set; }

        // 2. Foreign Key (Bağlantı) Alanları
        // DİKKAT: Veritabanındaki sütun adın 'Kullanicild' (küçük l) ise burayı da öyle yapmalısın.
        // Eğer ALTER TABLE ile 'KullaniciId' yaptıysan bu şekilde kalsın.
        public int? KullaniciId { get; set; }
        public int UrunId { get; set; }

        // 3. Ürün ve Satış Detayları
        public string BarkodNo { get; set; }
        public string Isim { get; set; } // Veritabanındaki 'Isim' sütunu
        public string Kategori { get; set; }
        public decimal SatisFiyati { get; set; }
        public int Adet { get; set; }

        // Veritabanındaki 'Total' sütunu (Adet * SatisFiyati sonucunun tutulduğu yer)
        public decimal Total { get; set; }

        // 4. JOIN Sorgusu ile Gelecek Alanlar
        // Bu alan 'satislar' tablosunda yoktur, SELECT sorgusunda 'k.AdSoyad as Kasiyer' dediğimiz için dolar.
        public string Kasiyer { get; set; }

        // 5. Tarih Bilgisi
        public DateTime SatisTarihi { get; set; }
        public int? MusteriId { get; set; }

        // Yardımcı Özellik: C# tarafında anlık hesaplama gerekirse kullanılır.
        public decimal HesaplanacakToplam => Adet * SatisFiyati;
    }
}
