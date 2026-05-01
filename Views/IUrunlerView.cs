using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Views
{
    public interface IUrunlerView
    {
        // TextBox'lardan alacağımız veriler (Sözleşme Maddeleri)
        string BarkodNo { get; set; }
        string UrunAdi { get; set; }
        string Kategori { get; set; }
        string AlisFiyati { get; set; }
        string SatisFiyati { get; set; }
        string StokMiktari { get; set; }
        string ResimYolu { get; set; }

        // Mesaj gösterme ve tabloyu doldurma işlemleri
        void MesajGoster(string mesaj);
        void SetUrunListesi(BindingSource urunListesi);
        void KategoriListesiniDoldur(System.Data.DataTable dt);

        // Kullanıcının butonlara tıklama olayları (Events)
        event EventHandler KaydetEvent;
        event EventHandler GuncelleEvent;
        event EventHandler SilEvent;
        event EventHandler AramaEvent;
    }
}
