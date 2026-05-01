using MySql.Data.MySqlClient;
using StokTakip.Dao;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StokTakip.Views
{
    public partial class UC_AnaSayfa : UserControl
    {
        private readonly string baglantiCumlesi = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd='';";
        private readonly CultureInfo trKultur = new CultureInfo("tr-TR");

        // UC_Satis'in bu sayfaya erişebilmesi için statik referans
        public static UC_AnaSayfa Nesne;

        public UC_AnaSayfa()
        {
            InitializeComponent();
            Nesne = this;

            // Designer açılırken veritabanına bağlanmayı denemesin.
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                TumVerileriGuncelle();
            }
        }

        private void UC_AnaSayfa_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                TumVerileriGuncelle();
            }
        }

        /// <summary>
        /// UC_Satis'ten veya Load olayından çağrılan ana yenileme merkezi
        /// </summary>
        public void TumVerileriGuncelle()
        {
            try
            {
                SatisOzetPanelleriniYukle();
                GercekVerilerleGrafikCiz();
                SatisListesiniGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Genel Güncelleme Hatası: " + ex.Message);
            }
        }

        private void SatisOzetPanelleriniYukle()
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                try
                {
                    baglanti.Open();

                    // 1) Toplam Satış: satış tablosundaki Total değerlerinin toplamı
                    decimal toplamSatis = ScalarDecimalGetir(
                        baglanti,
                        "SELECT IFNULL(SUM(Total), 0) FROM satislar"
                    );

                    // 2) Toplam Kazanç: (Satış Fiyatı - Alış Fiyatı) * Adet
                    decimal toplamKazanc = ScalarDecimalGetir(
                        baglanti,
                        @"SELECT IFNULL(SUM((s.SatisFiyati - IFNULL(u.AlisFiyati, 0)) * s.Adet), 0)
                          FROM satislar s
                          LEFT JOIN urunler u ON u.UrunID = s.UrunId"
                    );

                    // 3) Kritik Stok: stoğu 25'in altındaki aktif ürün adedi
                    int kritikStok = ScalarIntGetir(
                        baglanti,
                        "SELECT IFNULL(COUNT(*), 0) FROM urunler WHERE StokMiktari < 25 AND AktifMi = 1"
                    );

                    // 4) Sipariş Sayısı: toplam satış kaydı adedi (kaç kez satış girildiği)
                    int siparisSayisi = ScalarIntGetir(
                        baglanti,
                        "SELECT IFNULL(COUNT(*), 0) FROM satislar"
                    );

                    label_toplamSatisFiyati.Text = $"{toplamSatis.ToString("N2", trKultur)} ₺";
                    label_toplamKazancFiyati.Text = $"{toplamKazanc.ToString("N2", trKultur)} ₺";
                    label_kritikStokAdeti.Text = $"{kritikStok} Ürün";
                    label_siparisSayisiAdeti.Text = $"{siparisSayisi} Adet";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Özet Panel Hatası: " + ex.Message);
                }
            }
        }

        private decimal ScalarDecimalGetir(MySqlConnection baglanti, string sorgu)
        {
            using (MySqlCommand cmd = new MySqlCommand(sorgu, baglanti))
            {
                object sonuc = cmd.ExecuteScalar();
                return (sonuc == null || sonuc == DBNull.Value) ? 0m : Convert.ToDecimal(sonuc);
            }
        }

        private int ScalarIntGetir(MySqlConnection baglanti, string sorgu)
        {
            using (MySqlCommand cmd = new MySqlCommand(sorgu, baglanti))
            {
                object sonuc = cmd.ExecuteScalar();
                return (sonuc == null || sonuc == DBNull.Value) ? 0 : Convert.ToInt32(sonuc);
            }
        }

        private void SatisListesiniGetir()
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                try
                {
                    baglanti.Open();
                    string sql = @"SELECT Id, BarkodNo, Isim, Kategori, Adet, Total, SatisTarihi
                                   FROM satislar
                                   ORDER BY SatisTarihi DESC
                                   LIMIT 50";

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    guna2DataGridView1.DataSource = null;
                    guna2DataGridView1.Columns.Clear();
                    guna2DataGridView1.AutoGenerateColumns = true;
                    guna2DataGridView1.DataSource = dt;

                    if (guna2DataGridView1.Columns.Contains("Isim"))
                    {
                        guna2DataGridView1.Columns["Isim"].HeaderText = "Ürün Adı";
                    }

                    if (guna2DataGridView1.Columns.Contains("Total"))
                    {
                        guna2DataGridView1.Columns["Total"].HeaderText = "Toplam Tutar";
                        guna2DataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
                    }

                    if (guna2DataGridView1.Columns.Contains("SatisTarihi"))
                    {
                        guna2DataGridView1.Columns["SatisTarihi"].HeaderText = "Satış Tarihi";
                    }

                    TabloyuGuzellestir();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Satış Listesi Hatası: " + ex.Message);
                }
            }
        }

        private void TabloyuGuzellestir()
        {
            // DataGrid, kendi paneli içinde çalışmalı ki kartların üstünü kapatmasın.
            if (guna2DataGridView1.Parent != guna2Panel_sonSatislar)
            {
                Controls.Remove(guna2DataGridView1);
                guna2Panel_sonSatislar.Controls.Add(guna2DataGridView1);
            }

            guna2DataGridView1.Dock = DockStyle.Fill;
            guna2DataGridView1.BringToFront();
            guna2DataGridView1.Visible = true;

            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            guna2DataGridView1.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.Black;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridView1.DefaultCellStyle.BackColor = Color.White;
            guna2DataGridView1.ColumnHeadersVisible = true;
            guna2DataGridView1.Refresh();
        }

        public void GercekVerilerleGrafikCiz()
        {
            try
            {
                guna2Panel_grafik.Controls.Clear();

                Chart pastaGrafik = new Chart
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White
                };

                ChartArea alan = new ChartArea();
                pastaGrafik.ChartAreas.Add(alan);

                Series seri = new Series("Stoklar")
                {
                    ChartType = SeriesChartType.Pie,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.Black
                };
                seri["PieLabelStyle"] = "Outside";
                seri["PieLineColor"] = "Black";
                seri.LegendText = "#VALX";
                seri.Label = "#VALX\n#VALY Adet";

                UrunDao dao = new UrunDao();
                DataTable dt = dao.UrunBazliStokDagilimiGetir();

                if (dt != null && dt.Rows.Count > 0)
                {
                    bool enAzBirUrunEklendi = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        int stokAdedi = Convert.ToInt32(row["StokMiktari"]);
                        if (stokAdedi <= 0)
                        {
                            continue;
                        }

                        seri.Points.AddXY(
                            row["UrunAdi"]?.ToString(),
                            stokAdedi
                        );
                        enAzBirUrunEklendi = true;
                    }

                    if (!enAzBirUrunEklendi)
                    {
                        seri.Points.AddXY("Stok Yok", 1);
                        seri.Label = "Stok Yok";
                    }
                }
                else
                {
                    seri.Points.AddXY("Ürün Yok", 1);
                    seri.Label = "Ürün Yok";
                }

                pastaGrafik.Series.Add(seri);
                guna2Panel_grafik.Controls.Add(pastaGrafik);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grafik Yükleme Hatası: " + ex.Message);
            }
        }

        private void guna2Panel_sonSatislar_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
