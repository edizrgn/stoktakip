using StokTakip.Dao;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StokTakip.Views
{
    public partial class UC_Musteriler : UserControl
    {
        private readonly MusteriDao musteriDao = new MusteriDao();
        private int seciliMusteriId = 0;

        public UC_Musteriler()
        {
            InitializeComponent();
            ListeyiYenile();
        }

        private void ListeyiYenile()
        {
            List<Musteri> liste = musteriDao.MusterileriGetir();
            guna2DataGridView1.DataSource = liste;

            if (guna2DataGridView1.Columns.Count > 0)
            {
                DataGridViewColumn? idKolonu = guna2DataGridView1.Columns["Id"];
                if (idKolonu != null) idKolonu.Visible = false;

                DataGridViewColumn? adSoyadKolonu = guna2DataGridView1.Columns["AdSoyad"];
                if (adSoyadKolonu != null) adSoyadKolonu.HeaderText = "Ad Soyad";

                DataGridViewColumn? telefonKolonu = guna2DataGridView1.Columns["Telefon"];
                if (telefonKolonu != null) telefonKolonu.HeaderText = "Telefon";

                DataGridViewColumn? epostaKolonu = guna2DataGridView1.Columns["Eposta"];
                if (epostaKolonu != null) epostaKolonu.HeaderText = "E-Posta";

                DataGridViewColumn? adresKolonu = guna2DataGridView1.Columns["Adres"];
                if (adresKolonu != null) adresKolonu.HeaderText = "Adres";
            }

            guna2DataGridView1.ClearSelection();
        }

        private bool MusteriBilgileriGecerliMi()
        {
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtTelefon.Text) ||
                string.IsNullOrWhiteSpace(txtEposta.Text) ||
                string.IsNullOrWhiteSpace(txtAdres.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string emailSabloni = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEposta.Text, emailSabloni))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi girin! (Örn: ornek@mail.com)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string telefonSabloni = @"^[0-9]{10,11}$";
            if (!Regex.IsMatch(txtTelefon.Text, telefonSabloni))
            {
                MessageBox.Show("Lütfen geçerli bir telefon numarası girin! (Sadece rakam, boşluksuz, 10-11 hane)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void FormuTemizle()
        {
            seciliMusteriId = 0;
            txtAdSoyad.Clear();
            txtTelefon.Clear();
            txtEposta.Clear();
            txtAdres.Clear();
            guna2DataGridView1.ClearSelection();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (!MusteriBilgileriGecerliMi())
            {
                return;
            }

            Musteri yeniMusteri = new Musteri
            {
                AdSoyad = txtAdSoyad.Text,
                Telefon = txtTelefon.Text,
                Eposta = txtEposta.Text,
                Adres = txtAdres.Text
            };

            musteriDao.MusteriEkle(yeniMusteri);

            MessageBox.Show("Müşteri başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormuTemizle();
            ListeyiYenile();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliMusteriId <= 0)
            {
                MessageBox.Show("Lütfen güncellemek için listeden bir müşteri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!MusteriBilgileriGecerliMi())
            {
                return;
            }

            Musteri guncellenecekMusteri = new Musteri
            {
                Id = seciliMusteriId,
                AdSoyad = txtAdSoyad.Text,
                Telefon = txtTelefon.Text,
                Eposta = txtEposta.Text,
                Adres = txtAdres.Text
            };

            bool basarili = musteriDao.MusteriGuncelle(guncellenecekMusteri);
            if (basarili)
            {
                MessageBox.Show("Müşteri bilgileri güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                ListeyiYenile();
            }
            else
            {
                MessageBox.Show("Güncelleme yapılamadı. Müşteri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliMusteriId <= 0)
            {
                MessageBox.Show("Lütfen silmek için listeden bir müşteri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult sonuc = MessageBox.Show(
                "Seçili müşteriyi silmek istediğinize emin misiniz?",
                "Müşteri Sil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (sonuc != DialogResult.Yes)
            {
                return;
            }

            bool basarili = musteriDao.MusteriSil(seciliMusteriId);
            if (basarili)
            {
                MessageBox.Show("Müşteri başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                ListeyiYenile();
            }
            else
            {
                MessageBox.Show("Silme işlemi yapılamadı. Müşteri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow satir = guna2DataGridView1.Rows[e.RowIndex];
            if (satir.Cells["Id"]?.Value == null)
            {
                return;
            }

            seciliMusteriId = Convert.ToInt32(satir.Cells["Id"].Value);
            txtAdSoyad.Text = satir.Cells["AdSoyad"]?.Value?.ToString() ?? "";
            txtTelefon.Text = satir.Cells["Telefon"]?.Value?.ToString() ?? "";
            txtEposta.Text = satir.Cells["Eposta"]?.Value?.ToString() ?? "";
            txtAdres.Text = satir.Cells["Adres"]?.Value?.ToString() ?? "";
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
