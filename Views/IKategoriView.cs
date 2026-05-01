using System;
using System.Data;

namespace StokTakip.Views
{
    public interface IKategoriView
    {
        // Kutudaki ve seçili satırdaki veriler
        string KategoriAdi { get; set; }
        int SeciliKategoriId { get; set; }

        // Aşçının garsona vereceği komutlar
        void KategorileriListele(DataTable dt);
        void MesajGoster(string mesaj, bool basariliMi);

        // Buton tıklama olayları (Haberleşme kabloları)
        event EventHandler EkleButtonClicked;
        event EventHandler GuncelleButtonClicked;
        event EventHandler SilButtonClicked;
        event EventHandler DataGridViewSecimDegisti;
    }
}