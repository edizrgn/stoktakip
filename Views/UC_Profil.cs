using StokTakip.Models;
using StokTakip.Presenters;
using StokTakip.Views;
using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace StokTakip // Kendi namespace'ine dikkat et
{
    // IProfilView sözleşmesini ekledik!
    public partial class UC_Profil : UserControl, IProfilView
    {
        private ProfilPresenter _presenter;

        public UC_Profil()
        {
            InitializeComponent();
            // BURADAKİ _presenter satırını sildik!
        }

        private void UC_Profil_Load(object sender, EventArgs e)
        {
            // Presenter'ı sayfa tam ekrana yüklenirken başlatıyoruz!
            _presenter = new ProfilPresenter(this, Oturum.KullaniciID);
        }

        // --- IProfilView SÖZLEŞMESİNİN ŞARTLARI ---
        // Fazlalıklar silindi, sadece görünmezlik pelerini giymiş olanlar kaldı!

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Ad { get => txt_Ad.Text; set => txt_Ad.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Soyad { get => txt_Soyad.Text; set => txt_Soyad.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EskiSifre { get => txt_EskiSifre.Text; set => txt_EskiSifre.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string YeniSifre { get => txt_YeniSifre.Text; set => txt_YeniSifre.Text = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string YeniSifreTekrar { get => txt_YeniSifreTekrar.Text; set => txt_YeniSifreTekrar.Text = value; }

        public event EventHandler GuncelleButtonClicked;

        public void MesajGoster(string mesaj, bool basariliMi)
        {
            if (basariliMi)
                MessageBox.Show(mesaj, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // --- BUTON TIKLAMA OLAYI ---
        private void btn_Kaydet_Click(object sender, EventArgs e)
        {
            // Butona tıklandığında Presenter'a haber veriyoruz
            GuncelleButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void lblYeniSifreTekrar_Click(object sender, EventArgs e)
        {

        }

        private void lblKisiselBaslik_Click(object sender, EventArgs e)
        {

        }
    }
}