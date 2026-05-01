using StokTakip.Views;
using System;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;

namespace StokTakip
{
    public partial class UC_Kategoriler : UserControl, IKategoriView
    {
        public UC_Kategoriler()
        {
            InitializeComponent();
        }

        // --- SÖZLEŞME ŞARTLARI ---
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string KategoriAdi { get => txt_KategoriAdi.Text; set => txt_KategoriAdi.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SeciliKategoriId { get; set; } // Tablodan seçilen kategorinin ID'sini aklımızda tutacağız

        public event EventHandler EkleButtonClicked;
        public event EventHandler GuncelleButtonClicked;
        public event EventHandler SilButtonClicked;
        public event EventHandler DataGridViewSecimDegisti;

        // --- AŞÇININ EKRANA YAZDIRMA METOTLARI ---
        public void KategorileriListele(DataTable dt)
        {
            grid_Kategoriler.DataSource = dt;
            // ID sütununu gizleyelim ki kullanıcıyı yormasın, sadece ismi görsün
            if (grid_Kategoriler.Columns["Id"] != null)
            {
                grid_Kategoriler.Columns["Id"].Visible = false;
            }
        }

        public void MesajGoster(string mesaj, bool basariliMi)
        {
            if (basariliMi)
                MessageBox.Show(mesaj, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // --- BUTON TIKLAMALARI (AŞÇIYA HABER GÖNDERME) ---
        private void btn_Ekle_Click(object sender, EventArgs e)
        {
            EkleButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btn_Guncelle_Click(object sender, EventArgs e)
        {
            GuncelleButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btn_Sil_Click(object sender, EventArgs e)
        {
            SilButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        // Tablodan bir satıra tıklandığında içindeki bilgiyi TextBox'a çekmek için
        private void grid_Kategoriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = grid_Kategoriler.Rows[e.RowIndex];
                SeciliKategoriId = Convert.ToInt32(row.Cells["Id"].Value);
                KategoriAdi = row.Cells["KategoriAdi"].Value.ToString();

                DataGridViewSecimDegisti?.Invoke(this, EventArgs.Empty);
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}