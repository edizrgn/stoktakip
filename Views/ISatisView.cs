using System;
using System.Collections.Generic;
using System.Text;



namespace StokTakip.Views
{
    public interface ISatisView
    {
        string Isim { get; set; }
        int Adet { get; set; }
        decimal SatisFiyati { get; set; }

       

        // Sepet yönetimi ve UI güncellemeleri için
        void MesajGoster(string mesaj);
        void SepetiTemizle();
        void AnaSayfayıTetikle(); // Satış bitince ana sayfayı yenilemek için
        void StokListesiniYenile();
    }
}