using Guna.UI2.WinForms;
using StokTakip.Dao;
using StokTakip.Presenters;
using StokTakip.Views;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StokTakip
{
    public partial class UC_Urunler : UserControl, IUrunlerView
    {
        private readonly UrunlerPresenter _presenter;
        private readonly UrunDao _urunDao = new UrunDao();

        public UC_Urunler()
        {
            InitializeComponent();

            // Buton tıklama olaylarını Interface'deki Event'lere (Presenter'a) bağlıyoruz
            btn_Kaydet.Click += delegate { KaydetEvent?.Invoke(this, EventArgs.Empty); };
            btn_Guncelle.Click += delegate { GuncelleEvent?.Invoke(this, EventArgs.Empty); };
            btn_Sil.Click += delegate { SilEvent?.Invoke(this, EventArgs.Empty); };

            // Resim seçme butonunu kendi metoduna bağlıyoruz
            btn_ResimSec.Click += Btn_ResimSec_Click;

            // Presenter'ı başlat
            _presenter = new UrunlerPresenter(this);

            // Grafik sekmesini başlangıçta temiz başlat
            GrafikleriTemizle();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string BarkodNo { get => txt_Barkod.Text; set => txt_Barkod.Text = value; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UrunAdi { get => txt_UrunAdi.Text; set => txt_UrunAdi.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Kategori
        {
            get => cmb_Kategori.SelectedValue != null ? cmb_Kategori.SelectedValue.ToString() : "";
            set => cmb_Kategori.SelectedValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AlisFiyati { get => txt_AlisFiyati.Text; set => txt_AlisFiyati.Text = value; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SatisFiyati { get => txt_SatisFiyati.Text; set => txt_SatisFiyati.Text = value; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StokMiktari { get => txt_StokMiktari.Text; set => txt_StokMiktari.Text = value; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ResimYolu
        {
            get => pic_UrunResmi.ImageLocation;
            set => pic_UrunResmi.ImageLocation = value;
        }

        // Dışarıdan gelen barkodu direkt ekrandaki TextBox'a yazar
        public void BarkoduEkranaYaz(string okunanBarkod)
        {
            txt_Barkod.Text = okunanBarkod;
        }

        // --- TABLO DOLDURMA VE MESAJ METOTLARI ---
        public void SetUrunListesi(BindingSource urunListesi)
        {
            guna2DataGridView1.DataSource = urunListesi;

            if (guna2DataGridView1.Columns.Count > 0)
            {
                if (guna2DataGridView1.Columns["UrunID"] != null) guna2DataGridView1.Columns["UrunID"].Visible = false;
                if (guna2DataGridView1.Columns["BarkodNo"] != null) guna2DataGridView1.Columns["BarkodNo"].HeaderText = "Barkod";
                if (guna2DataGridView1.Columns["UrunAdi"] != null) guna2DataGridView1.Columns["UrunAdi"].HeaderText = "Ürün Adı";
                if (guna2DataGridView1.Columns["Kategori"] != null) guna2DataGridView1.Columns["Kategori"].HeaderText = "Kategori";
                if (guna2DataGridView1.Columns["AlisFiyati"] != null) guna2DataGridView1.Columns["AlisFiyati"].HeaderText = "Alış (₺)";
                if (guna2DataGridView1.Columns["SatisFiyati"] != null) guna2DataGridView1.Columns["SatisFiyati"].HeaderText = "Satış (₺)";
                if (guna2DataGridView1.Columns["StokMiktari"] != null) guna2DataGridView1.Columns["StokMiktari"].HeaderText = "Stok";
                if (guna2DataGridView1.Columns["ResimYolu"] != null) guna2DataGridView1.Columns["ResimYolu"].Visible = false;
                if (guna2DataGridView1.Columns["AktifMi"] != null) guna2DataGridView1.Columns["AktifMi"].Visible = false;
            }

            if (guna2DataGridView1.Rows.Count == 0)
            {
                GrafikleriTemizle();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = guna2DataGridView1.Rows[e.RowIndex];

                txt_Barkod.Text = satir.Cells["BarkodNo"]?.Value?.ToString() ?? "";
                txt_UrunAdi.Text = satir.Cells["UrunAdi"]?.Value?.ToString() ?? "";
                cmb_Kategori.Text = satir.Cells["Kategori"]?.Value?.ToString() ?? "";
                txt_AlisFiyati.Text = satir.Cells["AlisFiyati"]?.Value?.ToString() ?? "";
                txt_SatisFiyati.Text = satir.Cells["SatisFiyati"]?.Value?.ToString() ?? "";
                txt_StokMiktari.Text = satir.Cells["StokMiktari"]?.Value?.ToString() ?? "";

                string resimYolu = satir.Cells["ResimYolu"]?.Value?.ToString() ?? "";

                if (!string.IsNullOrEmpty(resimYolu) && System.IO.File.Exists(resimYolu))
                {
                    pic_UrunResmi.ImageLocation = resimYolu;
                }
                else
                {
                    pic_UrunResmi.Image = null;
                }

                SeciliUrunGrafikleriniYukle();
            }
        }

        public void MesajGoster(string mesaj)
        {
            MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- RESİM SEÇME İŞLEMİ ---
        private void Btn_ResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Ürün Resmi Seç";
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 1. Seçilen resmin yolunu PictureBox'a kaydet
                pic_UrunResmi.ImageLocation = ofd.FileName;

                // 2. MODERN DOKUNUŞ: Resmi kutuda gösterirken sündürme!
                pic_UrunResmi.SizeMode = PictureBoxSizeMode.Zoom;

                // 3. (Opsiyonel) Eğer resim seçildiyse butonu pasif yapıp "Resmi Değiştir" diyebilirsin.
                btn_ResimSec.Text = "Resmi Değiştir";
            }
        }

        private void btn_ResimSec_Click_1(object sender, EventArgs e)
        {

        }

        // --- INTERFACE'TEN GELEN OLAYLAR (EVENTS) ---
        public event EventHandler KaydetEvent;
        public event EventHandler GuncelleEvent;
        public event EventHandler SilEvent;
        public event EventHandler AramaEvent;

        public void KategoriListesiniDoldur(DataTable dt)
        {
            // 1. SİHİRLİ DOKUNUŞ: Gelen tabloya (dt) sahte bir satır oluşturuyoruz
            DataRow sahteSatir = dt.NewRow();
            // Veritabanındaki sütun isimlerine göre dolduruyoruz
            sahteSatir["Id"] = 0; // Veritabanında olmayan bir ID veriyoruz
            sahteSatir["KategoriAdi"] = "Lütfen Seçiniz...";

            // Bu sahte satırı listenin EN TEPESİNE (0. sıraya) yerleştiriyoruz
            dt.Rows.InsertAt(sahteSatir, 0);

            // 2. STANDART BAĞLAMA İŞLEMLERİ
            cmb_Kategori.DataSource = dt;
            cmb_Kategori.DisplayMember = "KategoriAdi";
            cmb_Kategori.ValueMember = "KategoriAdi";
            cmb_Kategori.DropDownStyle = ComboBoxStyle.DropDownList;

            // 3. İLK AÇILIŞ: Artık -1 yapıp boş bırakmak yerine, 0 yapıp "Lütfen Seçiniz..." yazısını gösteriyoruz
            cmb_Kategori.SelectedIndex = 0;

            cmb_Kategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Kategori.SelectedIndex = 0;

            // SAYFA İLK AÇILDIĞINDA YAZIYI SİLİK (GRİ) YAP:
            cmb_Kategori.ForeColor = Color.Gray;
        }

        private void cmb_Kategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Eğer 0. sıradaki "Lütfen Seçiniz..." seçiliyse yazıyı GRİ (silik) yap
            if (cmb_Kategori.SelectedIndex == 0)
            {
                cmb_Kategori.ForeColor = Color.Gray;
            }
            // Gerçek bir kategori seçildiyse yazıyı SİYAH yap
            else
            {
                cmb_Kategori.ForeColor = Color.Black;
            }
        }

        private void UC_Urunler_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SeciliUrunGrafikleriniYukle()
        {
            string barkod = txt_Barkod.Text.Trim();
            string urunAdi = txt_UrunAdi.Text.Trim();

            if (string.IsNullOrWhiteSpace(barkod))
            {
                GrafikleriTemizle();
                return;
            }

            lbl_SeciliUrun.Text = $"Seçili Ürün: {urunAdi} ({barkod})";

            DataTable satisGecmisi = _urunDao.UrunSatisAdetGecmisiGetir(barkod);
            DataTable stokGecmisi = _urunDao.UrunStokHareketGecmisiGetir(barkod);

            SutunGrafikDoldur(chart_SatisGecmisi, "SatisAdedi", satisGecmisi, "ToplamSatisAdedi", false);
            SutunGrafikDoldur(chart_StokGecmisi, "StokHareketi", stokGecmisi, "ToplamStokDegisimi", true);
        }

        private void GrafikleriTemizle()
        {
            lbl_SeciliUrun.Text = "Seçili Ürün: -";

            if (chart_SatisGecmisi.Series.Count > 0)
            {
                chart_SatisGecmisi.Series["SatisAdedi"].Points.Clear();
            }

            if (chart_StokGecmisi.Series.Count > 0)
            {
                chart_StokGecmisi.Series["StokHareketi"].Points.Clear();
            }
        }

        private void SutunGrafikDoldur(Chart grafik, string seriAdi, DataTable veri, string yKolonAdi, bool stokHareketiMi)
        {
            if (grafik.Series.Count == 0)
            {
                return;
            }

            Series seri = grafik.Series[seriAdi];
            seri.Points.Clear();

            if (veri == null || veri.Rows.Count == 0)
            {
                int index = seri.Points.AddXY("Veri Yok", 0);
                seri.Points[index].Label = "";
                return;
            }

            foreach (DataRow satir in veri.Rows)
            {
                string xEtiketi = "Tarih Yok";
                if (DateTime.TryParse(satir["Tarih"]?.ToString(), out DateTime tarih))
                {
                    xEtiketi = stokHareketiMi
                        ? tarih.ToString("dd.MM HH:mm")
                        : tarih.ToString("dd.MM.yy");
                }

                double yDegeri = 0;
                double.TryParse(satir[yKolonAdi]?.ToString(), out yDegeri);

                int noktaIndex = seri.Points.AddXY(xEtiketi, yDegeri);
                if (stokHareketiMi)
                {
                    seri.Points[noktaIndex].Color = yDegeri < 0
                        ? Color.FromArgb(239, 68, 68)
                        : Color.FromArgb(16, 185, 129);
                }
            }
        }
    }
}
