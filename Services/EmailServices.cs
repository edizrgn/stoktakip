using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace StokTakip.Services
{
    public class EmailServices
    {
        public string Gonder(string aliciMail)
        {
            try
            {
                // 1. RASTGELE 6 HANELİ KOD ÜRETİMİ
                Random rastgele = new Random();
                // 100000 ile 999999 arasında rastgele bir sayı üretip metne (string) çeviriyoruz
                string uretilenKod = rastgele.Next(100000, 1000000).ToString();

                // 2. MAİL İÇERİĞİNİ HAZIRLAMA
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("toygaryildiz34@gmail.com");
                mail.To.Add(aliciMail);

                mail.Subject = "Doğrulama Kodu";
                // Ürettiğimiz rastgele kodu mailin içine ekliyoruz:
                mail.Body = "Kayıt işlemini tamamlamak için güvenlik kodunuz: " + uretilenKod;

                // 3. SUNUCU VE ŞİFRE AYARLARI
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                // Buraya az önce oluşturduğun ve çalışan o 16 haneli yeni şifreni yazıyorsun
                client.Credentials = new NetworkCredential("toygaryildiz34@gmail.com", "");

                // 4. GÖNDERME İŞLEMİ
                client.Send(mail);

                // Bu rastgele kodu formda kontrol edebilmek için geri döndürüyoruz
                return uretilenKod;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderme hatası: " + ex.Message);
                return null;
            }
        }

        
        
    }
}
