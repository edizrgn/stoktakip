using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Views
{
    public interface IPersonelView
    {
        // Ekrandaki kutucuklara veri göndermek ve oradan veri almak için:
        string Ad { get; set; }
        string Soyad { get; set; }
        string Eposta { get; set; }
        string Sifre { get; set; }
        string Durum { get; set; }
        string KayitTarihi { get; set; }

        // Tabloyu (DataGridView) doldurmak için kullanılacak liste:
        object PersonelListesi { set; }

        // Butonlara basıldığında Presenter'a "bir işlem yap" demek için olaylar:
        event EventHandler PersonelGuncelle;
        event EventHandler PersonelBlokla;
        event EventHandler SeciliPersonelDegisti;
    }
}
