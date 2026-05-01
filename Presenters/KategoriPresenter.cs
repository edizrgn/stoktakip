using StokTakip.Dao;
using StokTakip.Views;
using System;
using System.Data;

namespace StokTakip.Presenters
{
    public class KategoriPresenter
    {
        private IKategoriView _view;
        private KategoriDao _dao;

        // Kurucu Metot: Sayfa açıldığında ilk burası çalışır
        public KategoriPresenter(IKategoriView view)
        {
            _view = view;
            _dao = new KategoriDao();

            // Garsonun butonlarına kulak misafiri oluyoruz (Kabloları bağlıyoruz)
            _view.EkleButtonClicked += OnEkleClicked;
            _view.GuncelleButtonClicked += OnGuncelleClicked;
            _view.SilButtonClicked += OnSilClicked;

            // Ekran açılır açılmaz tablodaki güncel listeyi getir
            KategorileriYukle();
        }

        // Tabloyu veritabanından çekip yenileyen metot
        private void KategorileriYukle()
        {
            DataTable dt = _dao.TumKategorileriGetir();
            _view.KategorileriListele(dt);
        }

        // EKLE BUTONUNA BASILINCA ÇALIŞACAK KOD
        private void OnEkleClicked(object sender, EventArgs e)
        {
            // Kutucuk boş mu diye kontrol et
            if (string.IsNullOrWhiteSpace(_view.KategoriAdi))
            {
                _view.MesajGoster("Lütfen bir kategori adı girin!", false);
                return;
            }

            string kategoriAdi = _view.KategoriAdi.Trim();

            // Aynı isimde kategori var mı? (Büyük/Küçük harf duyarsız)
            if (_dao.KategoriAdiVarMi(kategoriAdi))
            {
                _view.MesajGoster("Bu isimde bir kategori zaten var!", false);
                return;
            }

            // Depocuya eklemesini söyle
            bool basarili = _dao.KategoriEkle(kategoriAdi);
            if (basarili)
            {
                _view.MesajGoster("Kategori başarıyla eklendi!", true);
                _view.KategoriAdi = ""; // Kutuyu temizle
                KategorileriYukle(); // Tabloyu yenile ki yeni eklenen anında gözüksün
            }
        }

        // GÜNCELLE BUTONUNA BASILINCA ÇALIŞACAK KOD
        private void OnGuncelleClicked(object sender, EventArgs e)
        {
            // Tablodan bir satır seçilmiş mi?
            if (_view.SeciliKategoriId <= 0)
            {
                _view.MesajGoster("Lütfen güncellemek için tablodan bir kategori seçin!", false);
                return;
            }

            if (string.IsNullOrWhiteSpace(_view.KategoriAdi))
            {
                _view.MesajGoster("Kategori adı boş olamaz!", false);
                return;
            }

            string kategoriAdi = _view.KategoriAdi.Trim();

            // Güncellemede, seçili kayıt hariç aynı isimde başka bir kategori var mı?
            if (_dao.KategoriAdiVarMi(kategoriAdi, _view.SeciliKategoriId))
            {
                _view.MesajGoster("Bu isimde başka bir kategori zaten var!", false);
                return;
            }

            bool basarili = _dao.KategoriGuncelle(_view.SeciliKategoriId, kategoriAdi);
            if (basarili)
            {
                _view.MesajGoster("Kategori başarıyla güncellendi!", true);
                _view.KategoriAdi = "";
                _view.SeciliKategoriId = 0; // Seçimi sıfırla
                KategorileriYukle(); // Tabloyu yenile
            }
        }

        // SİL BUTONUNA BASILINCA ÇALIŞACAK KOD
        private void OnSilClicked(object sender, EventArgs e)
        {
            // Tablodan bir satır seçilmiş mi?
            if (_view.SeciliKategoriId <= 0)
            {
                _view.MesajGoster("Lütfen silmek için tablodan bir kategori seçin!", false);
                return;
            }

            bool basarili = _dao.KategoriSil(_view.SeciliKategoriId);
            if (basarili)
            {
                _view.MesajGoster("Kategori başarıyla silindi!", true);
                _view.KategoriAdi = "";
                _view.SeciliKategoriId = 0; // Seçimi sıfırla
                KategorileriYukle(); // Tabloyu yenile
            }
        }
    }
}
