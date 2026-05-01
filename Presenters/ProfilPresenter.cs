using System;
using System.Data;
using StokTakip.Views;
using StokTakip.Dao;

namespace StokTakip.Presenters
{
    public class ProfilPresenter
    {
        private const int MinimumSifreUzunlugu = 8;

        private IProfilView _view;
        private KullaniciDao _dao;
        private int _aktifKullaniciId;
        private string _mevcutSifre; // Veritabanındaki gerçek şifreyi hafızada tutacağız

        public ProfilPresenter(IProfilView view, int kullaniciId)
        {
            _view = view;
            _dao = new KullaniciDao();
            _aktifKullaniciId = kullaniciId;

            // Garsonun (View) butonuna tıklandığında benim haberim olsun diyoruz
            _view.GuncelleButtonClicked += OnGuncelleClicked;

            // Ekran açılır açılmaz bilgileri getir
            BilgileriYukle();
        }

        private void BilgileriYukle()
        {
            DataTable dt = _dao.KullaniciBilgileriniGetir(_aktifKullaniciId);

            if (dt.Rows.Count > 0)
            {
                // Veritabanından gelen tek parça ismi (AdSoyad) alıyoruz
                string tamAd = dt.Rows[0]["AdSoyad"].ToString();

                // İsmi ilk boşluk karakterinden ikiye bölüyoruz 
                // (Böylece iki isimli kişilerde sorun çıkmaz, örn: "Ali Can Yıldız" -> Ad: "Ali", Soyad: "Can Yıldız" olur)
                var isimParcalari = tamAd.Split(new char[] { ' ' }, 2);

                if (isimParcalari.Length > 1)
                {
                    _view.Ad = isimParcalari[0];
                    _view.Soyad = isimParcalari[1].Trim();
                }
                else
                {
                    // Eğer kullanıcı sadece "Toygar" yazmışsa (Soyadı yoksa) patlamasın diye
                    _view.Ad = tamAd;
                    _view.Soyad = "";
                }

                // Gerçek şifreyi gizlice hafızaya al (Eski şifreyi doğru girecek mi diye test etmek için)
                _mevcutSifre = dt.Rows[0]["Sifre"].ToString();
            }
        }

        private void OnGuncelleClicked(object sender, EventArgs e)
        {
            // EĞER KULLANICI ŞİFRE DEĞİŞTİRMEK İSTİYORSA KONTROLLERİ YAP
            if (!string.IsNullOrEmpty(_view.YeniSifre))
            {
                if (_view.EskiSifre.Trim().Length < MinimumSifreUzunlugu ||
                    _view.YeniSifre.Trim().Length < MinimumSifreUzunlugu ||
                    _view.YeniSifreTekrar.Trim().Length < MinimumSifreUzunlugu)
                {
                    _view.MesajGoster($"Şifre alanları en az {MinimumSifreUzunlugu} karakter olmalıdır.", false);
                    return;
                }

                // 1. Kural: Eski şifresini doğru yazmış mı?
                if (_view.EskiSifre != _mevcutSifre)
                {
                    _view.MesajGoster($"Eski şifre yanlış!", false);
                    return; // İşlemi durdur
                }

                // 2. Kural: Yeni yazdığı iki şifre birbiriyle aynı mı?
                if (_view.YeniSifre != _view.YeniSifreTekrar)
                {
                    _view.MesajGoster("Yeni şifreler birbiriyle uyuşmuyor!", false);
                    return; // İşlemi durdur
                }
            }

            // KUTULARDAKİ AD VE SOYADI BİRLEŞTİRİYORUZ (Çünkü veritabanı tek parça 'AdSoyad' istiyor)
            string birlesikAdSoyad = (_view.Ad.Trim() + " " + _view.Soyad.Trim()).Trim();

            // HER ŞEY TAMAMSA GÜNCELLEMEYİ YAP
            // Dikkat: Artık Ad ve Soyad yerine sadece 'birlesikAdSoyad' gönderiyoruz
            bool basarili = _dao.ProfilGuncelle(_aktifKullaniciId, birlesikAdSoyad, _view.YeniSifre);

            if (basarili)
            {
                _view.MesajGoster("Profil bilgileriniz başarıyla güncellendi!", true);

                // Şifre başarıyla değiştiyse, hafızadaki şifreyi de yenile ve kutuları temizle
                if (!string.IsNullOrEmpty(_view.YeniSifre))
                {
                    _mevcutSifre = _view.YeniSifre;
                    _view.EskiSifre = "";
                    _view.YeniSifre = "";
                    _view.YeniSifreTekrar = "";
                }
            }
            else
            {
                _view.MesajGoster("Güncelleme sırasında bir hata oluştu.", false);
            }
        }
    }
}
