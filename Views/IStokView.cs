using System;
using System.Data;

namespace StokTakip.Views
{
    public interface IStokView
    {
        // 1. Ekrandan (TextBox'lardan) Okunacak Veriler
        string BarkodNo { get; set; }
        int Miktar { get; set; }

        // 2. Ekrana Gönderilecek Veriler (Aşçı yemeği veriyor)
        void StokListesiniGoster(DataTable dt);
        void MesajGoster(string mesaj, bool basariliMi);

        // 3. Olaylar (Tetikleyiciler - Butonlara basıldığını bildiren alarmlar)
        event EventHandler AraButtonClicked;
        event EventHandler StokEkleButtonClicked;
        event EventHandler StokDusButtonClicked;
    }
}