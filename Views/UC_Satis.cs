using MySql.Data.MySqlClient;
using StokTakip.Dao;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StokTakip.Views
{
    public partial class UC_Satis : UserControl, ISatisView

    {
        private List<SepetDetay> sepetim = new List<SepetDetay>();
        private string baglantiCumlesi = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd='';";


        public class SepetDetay
        {
            public int UrunID { get; set; }
            public string BarkodNo { get; set; } // Barkod eklendi
            public string UrunAdi { get; set; } = string.Empty;
            public string Kategori { get; set; } // Kategori eklendi
            public int Adet { get; set; }
            public decimal SatisFiyati { get; set; }
            public decimal Toplam => Adet * SatisFiyati;
        }

        public UC_Satis()
        {
            InitializeComponent();
            StokListesiniGetir();
        }

        private void StokListesiniGetir(string aramaTerimi = "")
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                try
                {
                    baglanti.Open();
                    string sql = "SELECT UrunID, BarkodNo, UrunAdi, Kategori, StokMiktari, SatisFiyati FROM urunler WHERE AktifMi = 1 AND KullaniciID = @kullaniciId";

                    if (!string.IsNullOrEmpty(aramaTerimi))
                    {
                        sql += " AND (UrunAdi LIKE @ara OR BarkodNo LIKE @ara)";
                    }

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, baglanti);
                    da.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                    if (!string.IsNullOrEmpty(aramaTerimi))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@ara", "%" + aramaTerimi + "%");
                    }

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvStokListesi.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Stok Listesi Hatası: " + ex.Message); }
            }
        }

        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            if (dgvStokListesi.CurrentRow == null) return;

            try
            {
                var row = dgvStokListesi.CurrentRow;
                int adet = (int)numAdet.Value;
                if (adet <= 0) return;

                sepetim.Add(new SepetDetay
                {
                    UrunID = Convert.ToInt32(row.Cells["UrunID"].Value),
                    BarkodNo = row.Cells["BarkodNo"].Value?.ToString(),
                    UrunAdi = row.Cells["UrunAdi"].Value?.ToString(),
                    Kategori = row.Cells["Kategori"].Value?.ToString(),
                    SatisFiyati = Convert.ToDecimal(row.Cells["SatisFiyati"].Value),
                    Adet = adet
                });

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = sepetim;
            }
            catch (Exception ex) { MessageBox.Show("Sepete ekleme hatası: " + ex.Message); }
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {

            // 1. Sepet kontrolü
            if (sepetim == null || sepetim.Count == 0)
            {
                MessageBox.Show("Sepet boş, lütfen ürün ekleyin.");
                return;
            }

            try
            {
                int? seciliMusteriId = null;
                if (cmbMusteriler.SelectedValue != null && int.TryParse(cmbMusteriler.SelectedValue.ToString(), out int parsedMusteriId))
                {
                    seciliMusteriId = parsedMusteriId;
                }

                satislarDao satisDao = new satislarDao();
                UrunDao urunDao = new UrunDao();

                foreach (var urun in sepetim)
                {
                    // 2. Veritabanına Satış Kaydı Ekleme
                    var yeniSatis = new StokTakip.Models.satislar
                    {
                        UrunId = urun.UrunID,
                        KullaniciId = Oturum.KullaniciID,
                        BarkodNo = urun.BarkodNo,
                        Isim = urun.UrunAdi, // Veritabanındaki 'Isim' sütununa gider
                        Kategori = urun.Kategori,
                        SatisFiyati = urun.SatisFiyati,
                        Adet = urun.Adet,
                        Total = urun.Toplam,
                        SatisTarihi = DateTime.Now, // Satış tarihini o anki zaman olarak ayarla
                        MusteriId = seciliMusteriId
                    };

                    bool satisKaydiBasarili = satisDao.SatisEkle(yeniSatis);
                    if (!satisKaydiBasarili)
                    {
                        MessageBox.Show($"Satış kaydı yapılamadı. Ürün: {urun.UrunAdi}");
                        return;
                    }

                    // 3. Stok Miktarını Güncelleme (Stok Düşürme) + hareket geçmişine yazma
                    bool stokDusuruldu = urunDao.StokMiktariniDegistir(urun.BarkodNo, urun.Adet, false);
                    if (!stokDusuruldu)
                    {
                        MessageBox.Show($"Stok düşülemedi. Ürün: {urun.UrunAdi}");
                        return;
                    }
                }

                // 4. Başarı Mesajı ve Temizlik
                MessageBox.Show("Satış Onaylandı ve Stoklar Güncellendi!");

                // Sepeti temizler ve stok listesini (dgvStokListesi) yeniler
                TemizleVeYenile();

                // 5. ANA SAYFAYI UYANDIRMA (En Önemli Kısım)
                // Bellekteki UC_AnaSayfa nesnesini bulur ve içindeki listeyi/grafiği tazeler
                AnaSayfayıTetikle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satış İşlemi Sırasında Hata Oluştu: " + ex.Message);
            }
        }

        public void AnaSayfayıTetikle()
        {
            // Eğer Ana Sayfa daha önce en az bir kez açıldıysa bellektedir
            if (UC_AnaSayfa.Nesne != null)
            {
                UC_AnaSayfa.Nesne.TumVerileriGuncelle();
            }
        }

        // Panellerin içindeki alt kontrolleri de tarayan yardımcı fonksiyon
        private Control BulUC_AnaSayfa(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is UC_AnaSayfa) return c;

                // Eğer kontrolün içinde başka kontroller varsa (Panel gibi), onları da tara
                Control found = BulUC_AnaSayfa(c);
                if (found != null) return found;
            }
            return null;
        }



        // Parametreleri sildik, böylece her yerden kolayca çağırabilirsin
        private void TemizleVeYenile()
        {
            sepetim.Clear();
            dataGridView1.DataSource = null;
            StokListesiniGetir();
        }

        // --- ISatisView ve Diğer Olaylar ---
        string ISatisView.Isim { get => ""; set { } }
        int ISatisView.Adet { get => (int)numAdet.Value; set => numAdet.Value = value; }
        decimal ISatisView.SatisFiyati { get => 0; set { } }

        // Eğer barkod için bir TextBox kullanıyorsan onu bağla, yoksa boş bir değişken oluştur:
        string BarkodNo { get; set; } = string.Empty;

        public void MesajGoster(string mesaj) => MessageBox.Show(mesaj);
        public void SepetiTemizle() => TemizleVeYenile();

        // guna2Button1 için Click olayı
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (dgvStokListesi.CurrentRow == null || sepetim.Count == 0)
            {
                MessageBox.Show("Sepet boş veya ürün seçilmedi!");
                return;
            }

            try
            {
                var row = dgvStokListesi.CurrentRow;
                int urunId = Convert.ToInt32(row.Cells["UrunID"].Value);
                int silinecekAdet = (int)numAdet.Value;

                // Sepette bu ürünü bul
                var urun = sepetim.Find(x => x.UrunID == urunId);
                if (urun != null)
                {
                    if (silinecekAdet <= 0)
                    {
                        // Adet 0 ise ürünü tamamen sil
                        sepetim.Remove(urun);
                    }
                    else
                    {
                        // Adet > 0 ise belirtilen kadar azalt
                        urun.Adet -= silinecekAdet;

                        // Eğer kalan adet 0 veya altına düştüyse ürünü tamamen sil
                        if (urun.Adet <= 0)
                            sepetim.Remove(urun);
                    }

                    // DataGridView’i yenile
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = sepetim;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message);
            }
        }

        private void dgvStokListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void StokListesiniYenile()
        {
            StokListesiniGetir();
        }

        private void UC_Satis_Load(object sender, EventArgs e)
        {
            // Dao sınıfımızı çağırıyoruz
            MusteriDao musteriDao = new MusteriDao();

            // Müşterileri veritabanından çekiyoruz
            List<Musteri> musteriListesi = musteriDao.MusterileriGetir();

            // ComboBox'a bu listeyi bağlıyoruz
            cmbMusteriler.DataSource = musteriListesi;

            // Ekranda kullanıcının göreceği kısım (Müşterinin Adı Soyadı)
            cmbMusteriler.DisplayMember = "AdSoyad";

            // Arka planda programın hafızada tutacağı asıl veri (Müşterinin ID'si)
            cmbMusteriler.ValueMember = "Id";
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void btnFaturaKes_Click(object sender, EventArgs e)
        {
            // Baskı önizleme penceresini açar
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Yazı tiplerimizi belirliyoruz
            Font baslikFont = new Font("Arial", 16, FontStyle.Bold);
            Font altBaslikFont = new Font("Arial", 12, FontStyle.Regular);
            Font icerikFont = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush firca = new SolidBrush(Color.Black);

            int yEkseni = 50; // Yazdırmaya yukarıdan ne kadar boşluk bırakarak başlayacağımız

            // 1. Firma Başlığı
            e.Graphics.DrawString("STOK SAYIM PROJESİ SATIŞ FATURASI", baslikFont, firca, 200, yEkseni);
            yEkseni += 50;

            // 2. Tarih ve Müşteri Bilgisi
            e.Graphics.DrawString("Tarih: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"), altBaslikFont, firca, 50, yEkseni);
            e.Graphics.DrawString("Müşteri: " + cmbMusteriler.Text, altBaslikFont, firca, 500, yEkseni);
            yEkseni += 40;

            // 3. Çizgi Çekme
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 50, yEkseni, 750, yEkseni);
            yEkseni += 30;

            // 4. Sütun Başlıkları
            e.Graphics.DrawString("Ürün Adı", altBaslikFont, firca, 50, yEkseni);
            e.Graphics.DrawString("Adet", altBaslikFont, firca, 400, yEkseni);
            e.Graphics.DrawString("Fiyat", altBaslikFont, firca, 550, yEkseni);
            e.Graphics.DrawString("Tutar", altBaslikFont, firca, 650, yEkseni);
            yEkseni += 20;

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 50, yEkseni, 750, yEkseni);
            yEkseni += 30;

            // 5. Sepetteki Ürünleri Listeleme Döngüsü
            decimal genelToplam = 0;

            // DİKKAT: Aşağıdaki dgvSepet yazan yerleri kendi sepet tablonun adıyla değiştir (örneğin: guna2DataGridView2 vb.)
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // Tablonun en altındaki boş satırı yazdırmamak için kontrol ediyoruz
                if (!dataGridView1.Rows[i].IsNewRow && dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    // Hücrelerdeki verileri alıyoruz (Senin ekranındaki sütun sırasına göre: 1:İsim, 3:Adet, 4:Fiyat, 5:Total)
                    string urunAdi = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string adet = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string fiyat = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    string tutar = dataGridView1.Rows[i].Cells[5].Value.ToString();

                    // Faturaya alt alta yazdırıyoruz
                    e.Graphics.DrawString(urunAdi, icerikFont, firca, 50, yEkseni);
                    e.Graphics.DrawString(adet, icerikFont, firca, 400, yEkseni);
                    e.Graphics.DrawString(fiyat, icerikFont, firca, 550, yEkseni);
                    e.Graphics.DrawString(tutar, icerikFont, firca, 650, yEkseni);

                    yEkseni += 25; // Bir sonraki ürün için alt satıra geç

                    // Genel toplamı hesaplıyoruz (Tutar kısmını decimal'e çevirip topluyoruz)
                    genelToplam += Convert.ToDecimal(tutar);
                }
            }

            // 6. Genel Toplam ve Kapanış
            yEkseni += 20;
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 50, yEkseni, 750, yEkseni); // Alt çizgi
            yEkseni += 30;

            // Genel toplamı en alta büyük fontla yazdırıyoruz
            e.Graphics.DrawString("GENEL TOPLAM: " + genelToplam.ToString("C2"), baslikFont, firca, 450, yEkseni);
        }

        private void lblStokBaslik_Click(object sender, EventArgs e)
        {

        }
    }
}
