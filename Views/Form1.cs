using StokTakip.Dao;
using StokTakip.Models;
using StokTakip.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StokTakip
{
    public partial class Form1 : Form
    {
        private const int MinimumSifreUzunlugu = 8;

        // Mouse ile formu sürüklemek için gerekli değişkenler
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public Form1()
        {
            InitializeComponent();
            textBox_eposta.KeyDown += GirisIcinEnter_KeyDown;
            textBox_sifre.KeyDown += GirisIcinEnter_KeyDown;
        }

        // --- Form Sürükleme Olayları ---
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void pnlTop_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // --- Kapatma Butonu İşlemleri ---
        private void button_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            button_close.ForeColor = Color.White;
            button_close.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            button_close.ForeColor = Color.DimGray;
            button_close.BackColor = Color.Transparent;
        }

        // --- Giriş Yap Butonu ---
        private void button_giris_Click(object sender, EventArgs e)
        {

            string mail = textBox_eposta.Text.Trim();
            string sifre = textBox_sifre.Text.Trim();

            // E-posta kutusu boş mu veya içinde @ işareti ile nokta (.) yok mu?
            if (!textBox_eposta.Text.Contains("@") || !textBox_eposta.Text.Contains("."))
            {
                // Kullanıcıya uyarı ver
                MessageBox.Show("Lütfen geçerli bir e-posta adresi girin!\nÖrnek: isim@mail.com", "Geçersiz Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_eposta.Focus();
                return;
            }

            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sifre.Length < MinimumSifreUzunlugu)
            {
                MessageBox.Show($"Şifre en az {MinimumSifreUzunlugu} karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_sifre.Focus();
                return;
            }

            KullaniciDao kullaniciDao = new KullaniciDao();
            Kullanici girisYapan = kullaniciDao.GirisKontrol(mail, sifre);

            if (girisYapan != null)
            {
                Oturum.KullaniciID = girisYapan.Id;
                Oturum.AdSoyad = girisYapan.AdSoyad;

                
                // --- İŞTE SİHİRLİ YÖNLENDİRME KISMI BURASI ---
                // Veritabanından gelen "Rol" (Admin/Personel) ve "AdSoyad" bilgisini Dashboard'a fırlatıyoruz!
                DashboardForm dashboard = new DashboardForm(girisYapan.Rol, girisYapan.AdSoyad);
                dashboard.Show(); // Dashboard'u aç
                this.Hide();      // Bu giriş (Login) ekranını gizle
            }
            else
            {
                MessageBox.Show("E-posta veya şifre hatalı, ya da hesabınız bloklanmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel_kayit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Kayıt formundan bir nesne oluştur
            RegisterForm rf = new RegisterForm();

            // 2. Kayıt formunu göster
            rf.Show();

            // 3. Mevcut Giriş formunu gizle (Kapatırsan uygulama tamamen durabilir)
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // İhtiyaç varsa buraya kod yazılabilir
        }

        private void textBox_eposta_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_eposta_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox_sifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox_sifre_IconRightClick(object sender, EventArgs e)
        {
            // Eğer şifre şu an gizliyse (nokta noktaysa)
            if (textBox_sifre.UseSystemPasswordChar == true)
            {
                // Şifreyi görünür yap (gizliliği kapat)
                textBox_sifre.UseSystemPasswordChar = false;
                textBox_sifre.PasswordChar = '\0'; // Garanti olsun diye şifre karakterini sıfırlıyoruz

                // (İsteğe bağlı) Buraya "kapalı göz" ikonunu koyma kodu yazılabilir
            }
            else
            {
                // Şifre zaten görünürse, tekrar gizle
                textBox_sifre.UseSystemPasswordChar = true;

                // (İsteğe bağlı) Buraya "açık göz" ikonunu koyma kodu yazılabilir
            }
        }

        private void GirisIcinEnter_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button_giris_Click(button_giris, EventArgs.Empty);
            }
        }
    }
}
