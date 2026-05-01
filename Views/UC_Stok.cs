using StokTakip.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StokTakip.Views
{
    // DİKKAT: ': UserControl' yanına ', IStokView' ekliyoruz! Bu garsonun sözleşmeyi imzaladığını gösterir.
    public partial class UC_Stok : UserControl, IStokView
    {
        private StokPresenter _presenter;
        private readonly string _acilisBarkodu;
        private bool _acilisBarkoduUygulandi;

        public UC_Stok(string acilisBarkodu = "")
        {
            InitializeComponent();
            _acilisBarkodu = (acilisBarkodu ?? string.Empty).Trim();

            // Barkod, tabloda seçilen üründen otomatik doldurulsun.
            txt_BarkodAra.ReadOnly = true;
            txt_BarkodAra.PlaceholderText = "Tablodan ürün seçiniz";

            // Sistemi başlatıyoruz: Aşçıyı (Presenter) mutfağa alıyoruz ve bu ekranı (this) ona tanıtıyoruz.
            _presenter = new StokPresenter(this);
        }

        // --- 1. SÖZLEŞMEDEN GELEN VERİLER (Ekranda okunanlar) ---
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string BarkodNo
        {
            get { return txt_BarkodAra.Text.Trim(); } // Barkod kutusunun adı neyse buraya onu yaz
            set { txt_BarkodAra.Text = value; }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Miktar
        {
            get { return Convert.ToInt32(num_Miktar.Value); } // Sayı girilen kutunun adı
            set { num_Miktar.Value = value; }
        }

        // --- 2. SÖZLEŞMEDEN GELEN METOTLAR (Ekrana yansıtılanlar) ---
        public void StokListesiniGoster(DataTable dt)
        {
            dataGridView_Stok.DataSource = dt;

            // Tablodaki tüm sütunları tek tek gezip, sadece istediklerimizi açık bırakıyoruz
            foreach (DataGridViewColumn kolon in dataGridView_Stok.Columns)
            {
                // Kategori'yi de bu listeye ekledik!
                if (kolon.Name == "BarkodNo" || kolon.Name == "UrunAdi" || kolon.Name == "Kategori" || kolon.Name == "StokMiktari")
                {
                    kolon.Visible = true;
                }
                else
                {
                    kolon.Visible = false; // Geri kalanları (Alış/Satış fiyatı, Resim vb.) gizle
                }
            }

            // Başlıkları şık ve anlaşılır yapalım:
            if (dataGridView_Stok.Columns["BarkodNo"] != null) dataGridView_Stok.Columns["BarkodNo"].HeaderText = "Barkod Numarası";
            if (dataGridView_Stok.Columns["UrunAdi"] != null) dataGridView_Stok.Columns["UrunAdi"].HeaderText = "Ürün İsmi";
            if (dataGridView_Stok.Columns["Kategori"] != null) dataGridView_Stok.Columns["Kategori"].HeaderText = "Kategori";
            if (dataGridView_Stok.Columns["StokMiktari"] != null) dataGridView_Stok.Columns["StokMiktari"].HeaderText = "Mevcut Stok";

            if (!_acilisBarkoduUygulandi && !string.IsNullOrWhiteSpace(_acilisBarkodu))
            {
                BarkodaGoreSatirSec(_acilisBarkodu);
                BarkodNo = _acilisBarkodu;
                _acilisBarkoduUygulandi = true;
                return;
            }

            // Liste yenilendiğinde ilk/aktif satırdan barkodu otomatik al.
            SeciliSatirdanBarkoduDoldur();
        }

        public void MesajGoster(string mesaj, bool basariliMi)
        {
            MessageBoxIcon icon = basariliMi ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
            MessageBox.Show(mesaj, "Stok İşlemi", MessageBoxButtons.OK, icon);
        }

        // --- 3. SÖZLEŞMEDEN GELEN OLAYLAR (Alarmlar) ---
        public event EventHandler AraButtonClicked;
        public event EventHandler StokEkleButtonClicked;
        public event EventHandler StokDusButtonClicked;

        // --- 4. BUTON TIKLAMALARI ---
        // Yeşil "Stok Ekle" butonuna çift tıklayıp içine bunu yaz:
        private void btn_StokEkle_Click(object sender, EventArgs e)
        {
            StokEkleButtonClicked?.Invoke(this, EventArgs.Empty); // Aşçıya haber ver!
        }

        // Kırmızı "Stok Düş" butonuna çift tıklayıp içine bunu yaz:
        private void btn_StokDus_Click(object sender, EventArgs e)
        {
            StokDusButtonClicked?.Invoke(this, EventArgs.Empty); // Aşçıya haber ver!
        }

        private void chk_KritikStok_CheckedChanged(object sender, EventArgs e)
        {
            // 1. Önce tablodaki verileri bir paket (DataTable) olarak elimize alıyoruz
            DataTable dt = dataGridView_Stok.DataSource as DataTable;

            if (dt != null)
            {
                // 2. Eğer buton AÇIK ise (Checked == true)
                if (chk_KritikStok.Checked) // Butonun adı neyse onu yaz (Örn: guna2ToggleSwitch1)
                {
                    // Sadece StokMiktari 5 veya daha küçük olanları süzgeçten geçir!
                    dt.DefaultView.RowFilter = "StokMiktari <= 5";
                }
                // 3. Eğer buton KAPALI ise (Checked == false)
                else
                {
                    // Filtreyi temizle, herkesi geri getir!
                    dt.DefaultView.RowFilter = string.Empty;
                }

                // Filtre sonrası seçili satır değişebileceği için barkodu güncelle.
                SeciliSatirdanBarkoduDoldur();
            }
        }

        private void guna2HtmlLabel1_barkodNo_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_kritikStok_Click(object sender, EventArgs e)
        {

        }

        private void label_miktar_Click(object sender, EventArgs e)
        {

        }

        private void num_Miktar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_Stok_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_Stok_SelectionChanged(object sender, EventArgs e)
        {
            SeciliSatirdanBarkoduDoldur();
        }

        private void SeciliSatirdanBarkoduDoldur()
        {
            if (dataGridView_Stok.CurrentRow?.Cells["BarkodNo"]?.Value != null)
            {
                BarkodNo = Convert.ToString(dataGridView_Stok.CurrentRow.Cells["BarkodNo"].Value) ?? string.Empty;
                return;
            }

            BarkodNo = string.Empty;
        }

        private void BarkodaGoreSatirSec(string barkod)
        {
            if (string.IsNullOrWhiteSpace(barkod) || dataGridView_Stok.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow satir in dataGridView_Stok.Rows)
            {
                if (satir.IsNewRow)
                {
                    continue;
                }

                string satirBarkodu = Convert.ToString(satir.Cells["BarkodNo"]?.Value)?.Trim() ?? string.Empty;
                if (!string.Equals(satirBarkodu, barkod, StringComparison.Ordinal))
                {
                    continue;
                }

                dataGridView_Stok.ClearSelection();
                satir.Selected = true;

                if (satir.Cells["BarkodNo"] != null && satir.Cells["BarkodNo"].Visible)
                {
                    dataGridView_Stok.CurrentCell = satir.Cells["BarkodNo"];
                }
                else if (satir.Cells.Count > 0)
                {
                    dataGridView_Stok.CurrentCell = satir.Cells[0];
                }

                return;
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
