using StokTakip.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StokTakip
{
    public partial class DashboardForm : Form
    {
        public string KullaniciYetkisi;
        public string GirenKisiAdi;

        // Parantez içine string yetki ve string adSoyad ekledik:
        public DashboardForm(string yetki, string adSoyad)
        {
            InitializeComponent();

            // Dışarıdan gelen bilgileri kendi değişkenlerimize aktarıyoruz
            KullaniciYetkisi = yetki;
            GirenKisiAdi = adSoyad;

            UC_AnaSayfa anaSayfa = new UC_AnaSayfa();
            anaSayfa.Dock = DockStyle.Fill;
            pnl_AnaEkran.Controls.Add(anaSayfa);
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

            // 1. Sol üstteki isme, giriş yapan kişinin adını yazdır
            guna2Button_adSoyad.Text = GirenKisiAdi; // (Senin label'ın adı neyse onu yaz)

            // 2. YETKİ KONTROLÜ
            if (KullaniciYetkisi == "Admin")
            {
                guna2Button_personel.Visible = true; // Adminse butonu görsün
            }
            else
            {
                guna2Button_personel.Visible = false; // Personelse buton gizlensin
            }

            // İlk açıldığında başlık Ana Sayfa olsun
            lbl_SayfaBasligi.Text = "Ana Sayfa";

            pnl_AnaEkran.Controls.Clear();
            UC_AnaSayfa anaSayfa = new UC_AnaSayfa();
            anaSayfa.Dock = DockStyle.Fill;
            pnl_AnaEkran.Controls.Add(anaSayfa);
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label_anaSayfa_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void guna2DataGridView_sonatislar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        private void btn_BarkodOkut_Click(object sender, EventArgs e)
        {
            using (BarkodOkuyucuForm okuyucu = new BarkodOkuyucuForm())
            {
                if (okuyucu.ShowDialog() == DialogResult.OK)
                {
                    string barkod = okuyucu.OkunanBarkod;
                    bool kayitliMi = okuyucu.UrunKayitliMi;

                    if (kayitliMi)
                    {
                        UC_Stok stok = new UC_Stok(barkod);
                        SayfayiDegistir(stok, "Stok Yönetimi"); // Hem sayfayı hem ismini gönderdik!
                    }
                    else
                    {
                        UC_Urunler urun = new UC_Urunler();
                        urun.BarkoduEkranaYaz(barkod);
                        SayfayiDegistir(urun, "Ürün Kayıt Paneli");
                    }
                }
            }
        }

        // Ortak Sayfa Değiştirme Metodu (Panelinin adını kontrol et!)
        public void SayfayiDegistir(UserControl yeniSayfa, string sayfaBasligi)
        {
            // 1. Görünen başlığı güncelle
            lbl_SayfaBasligi.Text = sayfaBasligi;

            // Yazının değiştiğini Windows'a zorla bildir (Bazen ekran tazelenmez)
            pnl_Ust.Refresh();

            // 2. Sayfayı yükle
            if (pnl_AnaEkran.Controls.Count > 0)
                pnl_AnaEkran.Controls.Clear();

            yeniSayfa.Dock = DockStyle.Fill;
            pnl_AnaEkran.Controls.Add(yeniSayfa);
        }

        private void guna2Button_dashboard_Click(object sender, EventArgs e)
        {
            // BAŞLIĞI SIFIRLA
            lbl_SayfaBasligi.Text = "Ana Sayfa";

            pnl_AnaEkran.Controls.Clear();
            UC_AnaSayfa anaSayfa = new UC_AnaSayfa();
            anaSayfa.Dock = DockStyle.Fill;
            pnl_AnaEkran.Controls.Add(anaSayfa);
        }

        private void guna2Button_urunler_Click(object sender, EventArgs e)
        {
            // BAŞLIĞI DEĞİŞTİREN ASIL KOD BURASI:
            lbl_SayfaBasligi.Text = "Ürün Yönetimi";

            // Sayfa değiştirme işlemleri
            pnl_AnaEkran.Controls.Clear();
            UC_Urunler urunlerSayfasi = new UC_Urunler();
            urunlerSayfasi.Dock = DockStyle.Fill;
            pnl_AnaEkran.Controls.Add(urunlerSayfasi);
        }

        private void pnl_AnaEkran_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button_stok_Click(object sender, EventArgs e)
        {
            // 1. Önce ortadaki ana paneli temizliyoruz
            pnl_AnaEkran.Controls.Clear();

            // 2. Hazırladığımız o muhteşem Stok sayfasından bir tane oluşturuyoruz
            UC_Stok stokSayfasi = new UC_Stok();

            // 3. Sayfayı panelin içine tam sığacak şekilde yayıyoruz
            stokSayfasi.Dock = DockStyle.Fill;

            // 4. Ve paneli ekrana (Dashboard'un ortasına) ekliyoruz!
            pnl_AnaEkran.Controls.Add(stokSayfasi);
            lbl_SayfaBasligi.Text = "Stok Yönetimi";
        }

        private void guna2Button_satislar_Click(object sender, EventArgs e)
        {
            // 1. Önce ortadaki ana paneli temizliyoruz
            pnl_AnaEkran.Controls.Clear();

            // 2. Satış sayfasından (User Control) bir tane oluşturuyoruz
            UC_Satis satisSayfasi = new UC_Satis();

            // 3. Sayfayı panelin içine tam sığacak şekilde yayıyoruz
            satisSayfasi.Dock = DockStyle.Fill;

            // 4. Paneli ekrana ekliyoruz ve başlığı güncelliyoruz
            pnl_AnaEkran.Controls.Add(satisSayfasi);
            lbl_SayfaBasligi.Text = "Satış Yönetimi";
        }

        private void guna2Button_adSoyad_Click(object sender, EventArgs e)
        {
            // 1. Ortadaki paneli tamamen temizle (Eski sayfayı kaldır)
            pnl_AnaEkran.Controls.Clear(); // <--- BURASI DÜZELDİ

            // 2. Yeni profil sayfamızı (User Control) oluştur
            UC_Profil profilSayfasi = new UC_Profil();

            // 3. Sayfayı sündürerek panelin içine tam oturt
            profilSayfasi.Dock = DockStyle.Fill;

            // 4. Panelin içine ekle ve göster!
            pnl_AnaEkran.Controls.Add(profilSayfasi); // <--- BURASI DÜZELDİ

            // Başlığı da diğer sayfalar gibi güncelleyelim
            lbl_SayfaBasligi.Text = "Profil Ayarları"; // <--- BURASI DÜZELDİ
        }

        private void btn_Kategoriler_Click(object sender, EventArgs e)
        {
            // 1. Önce ortadaki ana paneli temizliyoruz
            pnl_AnaEkran.Controls.Clear();

            // 2. Yeni Kategoriler sayfamızı (User Control) oluşturuyoruz
            UC_Kategoriler kategorilerSayfasi = new UC_Kategoriler();

            // 3. Aşçıyı (Presenter) çağırıp Garsonla (View) tanıştırıyoruz
            // DİKKAT: Bunu yazmazsak sayfa açılır ama butonlar çalışmaz!
            new StokTakip.Presenters.KategoriPresenter(kategorilerSayfasi);

            // 4. Sayfayı panelin içine tam sığacak şekilde yayıyoruz
            kategorilerSayfasi.Dock = DockStyle.Fill;

            // 5. Paneli ekrana ekliyoruz ve üstteki başlığı güncelliyoruz
            pnl_AnaEkran.Controls.Add(kategorilerSayfasi);
            lbl_SayfaBasligi.Text = "Kategori Yönetimi";
        }

        private void guna2Button_personel_Click(object sender, EventArgs e)
        {
            // 1. Panelin içini temizle (Eski sayfa gitsin)
            pnl_AnaEkran.Controls.Clear();

            // 2. Personel sayfasını oluştur
            UC_Personeller uc = new UC_Personeller();

            // 3. Sayfayı panele tam sığacak şekilde ayarla
            uc.Dock = DockStyle.Fill;

            // 4. Sayfayı panele ekle ve öne getir
            pnl_AnaEkran.Controls.Add(uc);
            uc.BringToFront();


            lbl_SayfaBasligi.Text = "Personel Sayfası";
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // 1. Panelin içini temizle (Eski sayfa gitsin)
            pnl_AnaEkran.Controls.Clear();

            // 2. Personel sayfasını oluştur
            UC_Raporlar uc = new UC_Raporlar();

            // 3. Sayfayı panele tam sığacak şekilde ayarla
            uc.Dock = DockStyle.Fill;

            // 4. Sayfayı panele ekle ve öne getir
            pnl_AnaEkran.Controls.Add(uc);
            uc.BringToFront();


            lbl_SayfaBasligi.Text = "Raporlar Sayfası";

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // 1. Panelin içini temizle (Eski sayfa gitsin)
            pnl_AnaEkran.Controls.Clear();

            // 2. Personel sayfasını oluştur
            UC_Musteriler uc = new UC_Musteriler();

            // 3. Sayfayı panele tam sığacak şekilde ayarla
            uc.Dock = DockStyle.Fill;

            // 4. Sayfayı panele ekle ve öne getir
            pnl_AnaEkran.Controls.Add(uc);
            uc.BringToFront();


            lbl_SayfaBasligi.Text = "Müşteriler Sayfası";

        }

        private void guna2ControlBox_close_Click(object sender, EventArgs e)
        {

        }
    }
}
