using StokTakip.Dao;
using StokTakip.Models;
using StokTakip.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StokTakip.Views
{
    public partial class RegisterForm : Form
    {
        private const int MinimumSifreUzunlugu = 8;

        // Formu sürüklemek için değişkenler
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public RegisterForm()
        {
            InitializeComponent();
        }

        // --- Panel Sürükleme Kodları (pnlTop olaylarına bağlamayı unutma) ---
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

        // --- Kayıt Olma Mantığı ---
        private void button_kayitOl_Click(object sender, EventArgs e)
        {
            // E-posta kutusu boş mu veya içinde @ işareti ile nokta (.) yok mu?
            if (!textBox_eposta.Text.Contains("@") || !textBox_eposta.Text.Contains("."))
            {
                // Kullanıcıya uyarı ver
                MessageBox.Show("Lütfen geçerli bir e-posta adresi girin!\nÖrnek: isim@mail.com", "Geçersiz Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // İmleci tekrar e-posta kutusuna odakla ki düzeltmesi kolay olsun
                textBox_eposta.Focus();

                // return komutu çok kritiktir! Kodun aşağıya inip hatalı kayıt yapmasını bıçak gibi keser.
                return;
            }

            // ... (Senin diğer kayıt kodların buradan aşağıda devam edecek)
            // 1. Şifre eşleşmesi ve boş alan kontrolleri (senin yazdığın ilk kısım)
            if (textBox_sifre.Text.Trim().Length < MinimumSifreUzunlugu)
            {
                MessageBox.Show($"Şifre en az {MinimumSifreUzunlugu} karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_sifre.Focus();
                return;
            }

            if (textBox_sifre.Text != textBox_sifreTekrar.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor!");
                return;
            }

            // 2. KULLANICI NESNESİNİ OLUŞTUR (yeniUser hatasını çözer)
            Kullanici yeniUser = new Kullanici
            {
                AdSoyad = textBox_adSoyad.Text.Trim(),
                Eposta = textBox_eposta.Text.Trim(),
                Sifre = textBox_sifre.Text.Trim(),
                Rol = "Personel", // Artık ComboBox yok, herkes standart Personel!
                Durum = true
            };

            // 3. MAİLİ GÖNDER VE RASTGELE KODU AL (uretilenKod hatasını çözer)
            EmailServices emailService = new EmailServices();
            string uretilenKod = emailService.Gonder(yeniUser.Eposta);

            // Eğer mail gönderirken bir hata olduysa (Gonder metodu null döndürdüyse) işlemi durdur
            if (uretilenKod == null)
            {
                return;
            }

            // 4. DOĞRULAMA FORMUNU AÇ
            using (VerificationForm vf = new VerificationForm(uretilenKod))
            {
                vf.ShowDialog(); // Doğrulama formunun açılıp kapanmasını bekler

                // Eğer kullanıcı kodu doğru girdiyse (OnaylandiMi true ise)
                if (vf.OnaylandiMi == true)
                {
                    // 5. VERİTABANINA KAYDET
                    KullaniciDao dao = new KullaniciDao();
                    bool sonuc = dao.KayitEkle(yeniUser);

                    if (sonuc)
                    {
                        MessageBox.Show("Doğrulama başarılı! Kayıt başarıyla oluşturuldu.");
                        // Burada ana sayfaya veya login ekranına yönlendirme kodlarını yazabilirsin
                        // Kullanıcı mesaja "Tamam" dedikten sonra bu form (Kayıt ekranı) gizlenir
                        this.Hide();

                        // Giriş ekranı (Form1) yeniden oluşturulur ve ekranda gösterilir
                        Form1 girisEkrani = new Form1();
                        girisEkrani.Show();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt sırasında veritabanı hatası oluştu.");
                    }
                }
                else
                {
                    // Kullanıcı çarpıya basıp çıkarsa veya kodu yanlış girerse
                    MessageBox.Show("Doğrulama tamamlanmadığı için kayıt iptal edildi.");
                }
            }
        }



        private void button_close_Click_1(object sender, EventArgs e)
        {
            // Uygulamayı tamamen kapatır
            Application.Exit();
        }

        private void linkLabel_giris_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Yeni bir Giriş Formu (Form1) nesnesi oluştur
            // Not: Form1'in namespace'i farklıysa başına 'StokTakip.' eklemek hatayı çözer
            StokTakip.Form1 login = new StokTakip.Form1();

            // 2. Giriş formunu ekranda göster
            login.Show();

            // 3. Mevcut Kayıt Formunu (RegisterForm) kapat
            this.Close();
        }

        private void label_rolSecimi_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
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

        private void textBox_sifreTekrar_IconRightClick(object sender, EventArgs e)
        {
            // Eğer şifre şu an gizliyse (nokta noktaysa)
            if (textBox_sifreTekrar.UseSystemPasswordChar == true)
            {
                // Şifreyi görünür yap (gizliliği kapat)
                textBox_sifreTekrar.UseSystemPasswordChar = false;
                textBox_sifreTekrar.PasswordChar = '\0'; // Garanti olsun diye şifre karakterini sıfırlıyoruz

                // (İsteğe bağlı) Buraya "kapalı göz" ikonunu koyma kodu yazılabilir
            }
            else
            {
                // Şifre zaten görünürse, tekrar gizle
                textBox_sifreTekrar.UseSystemPasswordChar = true;

                // (İsteğe bağlı) Buraya "açık göz" ikonunu koyma kodu yazılabilir
            }
        }
    }
}
