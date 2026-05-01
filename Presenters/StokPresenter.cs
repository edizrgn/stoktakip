using StokTakip.Dao;
using StokTakip.Views;
using System;

namespace StokTakip.Presenters
{
    public class StokPresenter
    {
        private IStokView _view;
        private UrunDao _dao; // Zaten elimizde olan veritabanı sınıfını kullanıyoruz

        public StokPresenter(IStokView view)
        {
            _view = view;
            _dao = new UrunDao();

            // Garsonun (View) butonlarına alarm (Event) takıyoruz
            _view.StokEkleButtonClicked += StokEkle;
            _view.StokDusButtonClicked += StokDus;

            // Sayfa açılır açılmaz listeyi doldur
            ListeyiYenile();
        }

        private void ListeyiYenile()
        {
            // Kullanıcının kendi ürünlerini getirip ekrandaki tabloya basıyoruz
            _view.StokListesiniGoster(_dao.TumUrunleriGetir());
        }

        private void StokEkle(object sender, EventArgs e)
        {
            // Kontrol: Barkod boş mu? Miktar 0'dan büyük mü?
            if (string.IsNullOrWhiteSpace(_view.BarkodNo) || _view.Miktar <= 0)
            {
                _view.MesajGoster("Lütfen geçerli bir barkod ve 0'dan büyük bir miktar girin!", false);
                return;
            }

            // Veritabanında stoğu artır (Bu metodu birazdan Dao'ya ekleyeceğiz)
            bool sonuc = _dao.StokMiktariniDegistir(_view.BarkodNo, _view.Miktar, true);

            if (sonuc)
            {
                _view.MesajGoster("Stok başarıyla eklendi!", true);
                ListeyiYenile(); // Tabloyu anında güncelle
            }
            else
            {
                _view.MesajGoster("Ürün bulunamadı! Lütfen barkodu kontrol edin.", false);
            }
        }

        private void StokDus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.BarkodNo) || _view.Miktar <= 0)
            {
                _view.MesajGoster("Lütfen geçerli bir barkod ve 0'dan büyük bir miktar girin!", false);
                return;
            }

            // Veritabanında stoğu düş (true yerine false gönderiyoruz ki azaltsın)
            bool sonuc = _dao.StokMiktariniDegistir(_view.BarkodNo, _view.Miktar, false);

            if (sonuc)
            {
                _view.MesajGoster("Stok başarıyla düşüldü!", true);
                ListeyiYenile();
            }
            else
            {
                _view.MesajGoster("İşlem başarısız! Stok yetersiz veya ürün yok.", false);
            }
        }
    }
}