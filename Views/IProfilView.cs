using System;

namespace StokTakip.Views // Kendi projene göre namespace'i kontrol etmeyi unutma
{
    public interface IProfilView
    {
        // 1. Ekrandan (TextBox'lardan) Okunacak Veriler
        string Ad { get; set; }
        string Soyad { get; set; }
        string EskiSifre { get; set; }
        string YeniSifre { get; set; }
        string YeniSifreTekrar { get; set; }

        // 2. Ekrana Gönderilecek Veriler (Aşçı yemeği veriyor)
        void MesajGoster(string mesaj, bool basariliMi);

        // 3. Olaylar (Butona basıldığını bildiren alarm)
        event EventHandler GuncelleButtonClicked;
    }
}