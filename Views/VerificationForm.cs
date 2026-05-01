using StokTakip.Services;
using System;
using System.Windows.Forms;

namespace StokTakip.Views
{
    // partial class olduğundan emin ol
    public partial class VerificationForm : Form
    {
        private string asilKod;
        public bool OnaylandiMi = false;
        private object kullaniciEmailAdresi;

        // Yapıcı metot (Constructor)
        public VerificationForm(string kod)
        {
            InitializeComponent(); // Burası kırmızıysa Designer.cs dosyasıyla isim çakışması vardır
            asilKod = kod;
        }

        private void button_onayla_Click(object sender, EventArgs e)
        {
            // Çift tıkladığında oluşan metodun adı neyse (btnOnayla_Click vb.) onun içine yaz:
            {
                // txtKod yazan yer senin metin kutunun adıyla aynı olmalı
                if (textBox_kod.Text.Trim() == asilKod.Trim())
                {
                    this.OnaylandiMi = true;
                    MessageBox.Show("Tebrikler, Kod Doğrulandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Formun olumlu kapandığını belirtir
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hatalı kod girdiniz, lütfen tekrar kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Butonun üzerine fare ile gelince
        private void btnOnayla_MouseEnter(object sender, EventArgs e)
        {
            button_onayla.BackColor = Color.DeepSkyBlue; // Biraz daha açık bir mavi
        }

        // Butonun üzerinden fare ayrılınca
        private void btnOnayla_MouseLeave(object sender, EventArgs e)
        {
            button_onayla.BackColor = Color.DodgerBlue; // Eski haline dönsün
        }

        private void VerificationForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel_girisYap_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_dogrulama_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel_tekrarGonder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}