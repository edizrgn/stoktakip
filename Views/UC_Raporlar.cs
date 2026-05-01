using ClosedXML.Excel;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient; // Eğer MySQL kullanıyorsan
using StokTakip.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace StokTakip.Views
{
    public partial class UC_Raporlar : UserControl
    {
        private void UC_Raporlar_Load(object sender, EventArgs e)
        {

            // --- 2. COMBO KATEGORİ (VERİTABANINDAN) ---
            KategorileriDoldur();

            // --- 3. COMBO URUN (VERİTABANINDAN) ---
            UrunleriDoldur();
            // Tarihleri manuel (el ile) ekliyoruz
            TarihleriDoldur();

        }


        private void TarihleriDoldur()
        {
            comboTarih.Items.Clear();
            comboTarih.Items.Add("Tüm Zamanlar");
            comboTarih.Items.Add("Son 1 Ay");
            comboTarih.Items.Add("Son 3 Ay");
            comboTarih.Items.Add("Son 6 Ay");
            comboTarih.SelectedIndex = 0;
        }

        private void KategorileriDoldur()
        {
            try
            {
                // Bağlantı cümlesindeki Database isminin doğru olduğundan emin ol
                string baglantiCumlesi = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd='';";

                using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu = "SELECT KategoriAdi FROM Kategoriler";
                    MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                    MySqlDataReader oku = komut.ExecuteReader();

                    // ÖNCE TEMİZLE (Elle girdiklerin de dahil her şey gider)
                    comboKategori.Items.Clear();

                    // MANUEL OLARAK "Tümü" EKLE (Büyük T ile)
                    comboKategori.Items.Add("Tümü");

                    // VERİTABANINDAN GELENLERİ EKLE
                    while (oku.Read())
                    {
                        if (oku["KategoriAdi"] != DBNull.Value)
                        {
                            string kat = oku["KategoriAdi"].ToString();
                            // Eğer veritabanında yanlışlıkla "Tümü" diye kategori varsa mükerrer eklemesin
                            if (kat != "Tümü")
                            {
                                comboKategori.Items.Add(kat);
                            }
                        }
                    }

                    // İLK SIRADAKİNİ (Tümü) SEÇ
                    if (comboKategori.Items.Count > 0)
                        comboKategori.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // Hata olsa bile en azından "Tümü" seçeneği çıksın
                comboKategori.Items.Clear();
                comboKategori.Items.Add("Tümü");
                comboKategori.SelectedIndex = 0;
                MessageBox.Show("Kategoriler yüklenirken hata oluştu: " + ex.Message);
            }
        }
        private void UrunleriDoldur(string kategoriAdi = "")
        {
            try
            {
                string baglantiCumlesi = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd='';";
                using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    // 1. Temel sorgu
                    string sorgu = "SELECT UrunAdi FROM urunler";

                    // 2. DOĞRU SÜTUN ADI: urunler tablosunda sütun 'Kategori' olduğu için burayı düzelttik
                    if (!string.IsNullOrEmpty(kategoriAdi) && kategoriAdi != "Tümü")
                    {
                        sorgu += " WHERE Kategori = @kategori";
                    }

                    MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                    if (!string.IsNullOrEmpty(kategoriAdi) && kategoriAdi != "Tümü")
                    {
                        komut.Parameters.AddWithValue("@kategori", kategoriAdi);
                    }

                    MySqlDataReader oku = komut.ExecuteReader();

                    comboUrun.Items.Clear();
                    comboUrun.Items.Add("Tümü");



                    while (oku.Read())
                    {
                        if (oku["UrunAdi"] != DBNull.Value)
                        {
                            comboUrun.Items.Add(oku["UrunAdi"].ToString());
                        }
                    }

                    if (comboUrun.Items.Count > 0)
                        comboUrun.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün yükleme hatası: " + ex.Message);
            }
        }






        public UC_Raporlar()
        {
            InitializeComponent();
        }

        private void formsPlot3_Load(object sender, EventArgs e)
        {

        }

        private void Raporlar_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // 1. Seçim kontrolü
            if (comboTarih.SelectedItem == null || comboKategori.SelectedItem == null || comboUrun.SelectedItem == null)
            {
                MessageBox.Show("Lütfen tüm filtreleri seçiniz!", "Uyarı");
                return;
            }

            string secilenTarih = comboTarih.Text;
            string secilenKategori = comboKategori.Text;
            string secilenUrun = comboUrun.Text;

            // 2. JOIN Sorgusu 
            string sorgu = "SELECT s.*, u.AlisFiyati, (s.Total - (u.AlisFiyati * s.Adet)) AS NetKar FROM satislar s JOIN urunler u ON s.Isim = u.UrunAdi WHERE 1=1";

            // 3. Tarih Filtreleri
            if (secilenTarih == "Bugün") sorgu += " AND DATE(s.SatisTarihi) = CURDATE()";
            else if (secilenTarih == "Son 1 Hafta") sorgu += " AND s.SatisTarihi >= DATE_SUB(NOW(), INTERVAL 1 WEEK)";
            else if (secilenTarih == "Son 1 Ay") sorgu += " AND s.SatisTarihi >= DATE_SUB(NOW(), INTERVAL 1 MONTH)";
            else if (secilenTarih == "Son 3 Ay") sorgu += " AND s.SatisTarihi >= DATE_SUB(NOW(), INTERVAL 3 MONTH)";
            else if (secilenTarih == "Son 6 Ay") sorgu += " AND s.SatisTarihi >= DATE_SUB(NOW(), INTERVAL 6 MONTH)";

            // 4. Kategori ve Ürün Filtresi
            if (secilenKategori != "Tümü") sorgu += " AND s.Kategori = @kategori";
            if (secilenUrun != "Tümü") sorgu += " AND s.Isim = @urun";

            // 5. Sıralama
            string siralamaSutunu = "s.Total";
            if (rbArtan.Checked) sorgu += $" ORDER BY {siralamaSutunu} ASC";
            else if (rbAzalan.Checked) sorgu += $" ORDER BY {siralamaSutunu} DESC";

            string baglantiCumlesi = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
                {
                    MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                    if (secilenKategori != "Tümü") komut.Parameters.AddWithValue("@kategori", secilenKategori);
                    if (secilenUrun != "Tümü") komut.Parameters.AddWithValue("@urun", secilenUrun);

                    MySqlDataAdapter da = new MySqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Verileri Tabloya Bas
                    Guna2DataGridView.DataSource = dt;

                    // Sütun başlıkları ve gizleme işlemleri
                    if (dt.Rows.Count > 0)
                    {
                        if (Guna2DataGridView.Columns.Contains("UrunId")) Guna2DataGridView.Columns["UrunId"].HeaderText = "Ürün ID";
                        if (Guna2DataGridView.Columns.Contains("BarkodNo")) Guna2DataGridView.Columns["BarkodNo"].HeaderText = "Barkod";
                        if (Guna2DataGridView.Columns.Contains("Isim")) Guna2DataGridView.Columns["Isim"].HeaderText = "Ürün Adı";
                        if (Guna2DataGridView.Columns.Contains("Kategori")) Guna2DataGridView.Columns["Kategori"].HeaderText = "Kategori";
                        if (Guna2DataGridView.Columns.Contains("SatisFiyati")) Guna2DataGridView.Columns["SatisFiyati"].HeaderText = "Birim Fiyat";
                        if (Guna2DataGridView.Columns.Contains("Adet")) Guna2DataGridView.Columns["Adet"].HeaderText = "Miktar";
                        if (Guna2DataGridView.Columns.Contains("Total")) Guna2DataGridView.Columns["Total"].HeaderText = "Toplam Satış";
                        if (Guna2DataGridView.Columns.Contains("SatisTarihi")) Guna2DataGridView.Columns["SatisTarihi"].HeaderText = "Satış Tarihi";
                        if (Guna2DataGridView.Columns.Contains("AlisFiyati")) Guna2DataGridView.Columns["AlisFiyati"].HeaderText = "Birim Maliyet";
                        if (Guna2DataGridView.Columns.Contains("NetKar")) Guna2DataGridView.Columns["NetKar"].HeaderText = "Net Kâr";

                        if (Guna2DataGridView.Columns.Contains("id")) Guna2DataGridView.Columns["id"].Visible = false;
                        if (Guna2DataGridView.Columns.Contains("MusteriId")) Guna2DataGridView.Columns["MusteriId"].Visible = false;
                    }
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Buradaki ismi tasarımda verdiğin isimle değiştir
            if (comboKategori.SelectedItem != null)
            {
                string secilenKategori = comboKategori.SelectedItem.ToString();
                UrunleriDoldur(secilenKategori);
            }

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void formsPlot2_Load(object sender, EventArgs e)
        {


        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {


        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2RadioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            // 1. Tabloda veri var mı kontrol et
            if (Guna2DataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Aktarılacak veri bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Yeni bir Excel Çalışma Kitabı oluştur (ClosedXML)
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Satislar");

                    // 3. Başlıkları Aktar
                    for (int i = 0; i < Guna2DataGridView.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = Guna2DataGridView.Columns[i].HeaderText;
                        // Başlıkları biraz süsleyelim (isteğe bağlı)
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    }

                    // 4. Verileri Aktar
                    for (int r = 0; r < Guna2DataGridView.Rows.Count; r++)
                    {
                        for (int c = 0; c < Guna2DataGridView.Columns.Count; c++)
                        {
                            var hucreDegeri = Guna2DataGridView.Rows[r].Cells[c].Value;
                            if (hucreDegeri != null)
                            {
                                // r + 2 yapıyoruz çünkü 1. satırda başlıklar var
                                worksheet.Cell(r + 2, c + 1).Value = hucreDegeri.ToString();
                            }
                        }
                    }

                    // 5. Görsel Düzenleme
                    worksheet.Columns().AdjustToContents(); // Sütunları otomatik genişlet

                    // 6. Dosyayı Kaydetme Diyaloğu
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel Dosyası|*.xlsx";
                    sfd.FileName = "Satis_Raporu_" + DateTime.Now.ToString("dd_MM_yyyy");

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Excel başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // İstersen kaydedilen dosyayı otomatik açabilirsin:
                        // System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel aktarımı sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2RadioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void satisGrafigi_Click(object sender, EventArgs e)
        {

        }
    }
}
