using StokTakip.Dao;
using StokTakip.Models;
using StokTakip.Views;
using System;
using System.Windows.Forms;

namespace StokTakip.Presenters
{
    public class UrunlerPresenter
    {
        private readonly IUrunlerView _view;
        private readonly UrunDao _dao;

        public UrunlerPresenter(IUrunlerView view)
        {
            _view = view;
            _dao = new UrunDao();

            _view.KaydetEvent += UrunKaydet;
            _view.GuncelleEvent += UrunGuncelle;
            _view.SilEvent += UrunSil;

            UrunListesiniYenile();

            // Kategorileri veritabanından çekip ComboBox'a dolduruyoruz
            KategoriDao kategoriDao = new KategoriDao();
            _view.KategoriListesiniDoldur(kategoriDao.TumKategorileriGetir());
        }

        private void UrunKaydet(object sender, EventArgs e)
        {
            // --- GÜVENLİK KONTROLÜ: Kategori Seçilmiş Mi? ---
            if (string.IsNullOrWhiteSpace(_view.Kategori) || _view.Kategori == "Lütfen Seçiniz...")
            {
                _view.MesajGoster("Lütfen geçerli bir kategori seçiniz!");
                return; // Kaydetme işlemini iptal et ve aşağıya geçme
            }

            // Ekranda yazan bilgileri Model'e aktar
            Urun yeniUrun = new Urun
            {
                BarkodNo = _view.BarkodNo,
                UrunAdi = _view.UrunAdi,
                Kategori = _view.Kategori,
                AlisFiyati = decimal.Parse(_view.AlisFiyati),
                SatisFiyati = decimal.Parse(_view.SatisFiyati),
                StokMiktari = int.Parse(_view.StokMiktari),
                ResimYolu = _view.ResimYolu,
                KullaniciID = Oturum.KullaniciID
            };

            // Veritabanına gönder
            if (_dao.UrunEkle(yeniUrun))
            {
                _view.MesajGoster("Ürün başarıyla kaydedildi!");
                UrunListesiniYenile();
            }
            else
            {
                _view.MesajGoster("Hata: Ürün kaydedilemedi.");
            }
        }

        private void UrunGuncelle(object sender, EventArgs e)
        {
            // --- GÜVENLİK KONTROLÜ: Kategori Seçilmiş Mi? ---
            if (string.IsNullOrWhiteSpace(_view.Kategori) || _view.Kategori == "Lütfen Seçiniz...")
            {
                _view.MesajGoster("Lütfen geçerli bir kategori seçiniz!");
                return; // Güncelleme işlemini iptal et
            }

            Urun guncelUrun = new Urun
            {
                BarkodNo = _view.BarkodNo,
                UrunAdi = _view.UrunAdi,
                Kategori = _view.Kategori,
                AlisFiyati = decimal.Parse(_view.AlisFiyati),
                SatisFiyati = decimal.Parse(_view.SatisFiyati),
                StokMiktari = int.Parse(_view.StokMiktari),
                ResimYolu = _view.ResimYolu
            };

            if (_dao.UrunGuncelle(guncelUrun))
            {
                _view.MesajGoster("Ürün başarıyla güncellendi!");
                UrunListesiniYenile();
            }
            else
            {
                _view.MesajGoster("Hata: Ürün güncellenemedi.");
            }
        }

        private void UrunSil(object sender, EventArgs e)
        {
            if (_dao.UrunSil(_view.BarkodNo))
            {
                _view.MesajGoster("Ürün başarıyla silindi!");
                UrunListesiniYenile();
            }
            else
            {
                _view.MesajGoster("Hata: Ürün silinemedi.");
            }
        }

        private void UrunListesiniYenile()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = _dao.TumUrunleriGetir();
            _view.SetUrunListesi(bs);
        }
    }
}