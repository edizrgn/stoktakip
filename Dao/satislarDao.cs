using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms; // DİKKAT: MessageBox kullanabilmek için bu satır ŞART!

namespace StokTakip.Dao
{
    public class satislarDao
    {
        private string connectionString = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd='';";
        public DataTable TumSatislariGetir()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT Id, BarkodNo, Isim, Kategori, SatisFiyati, Adet, Total, SatisTarihi 
                   FROM satislar 
                   ORDER BY SatisTarihi DESC";

            using (MySqlConnection baglanti = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, baglanti);
                    baglanti.Open();
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri çekme hatası (DAO): " + ex.Message);
                }
            }
            return dt;
        }


        // Yeni satış eklemek için bu metodu kullanmalısın
        // Yeni satış eklemek için bu metodu kullanmalısın
        public bool SatisEkle(StokTakip.Models.satislar yeniSatis)
        {
            using (MySqlConnection baglanti = new MySqlConnection(connectionString))
            {
                // SQL sorgusuna MusteriId sütununu ve @musteriId parametresini ekledik
                string sql = @"INSERT INTO satislar 
               (KullaniciId, UrunId, BarkodNo, Isim, Kategori, SatisFiyati, Adet, Total, SatisTarihi, MusteriId) 
               VALUES 
               (@kullaniciId, @uId, @barkod, @isim, @kategori, @fiyat, @adet, @total, @tarih, @musteriId)";

                MySqlCommand cmd = new MySqlCommand(sql, baglanti);

                cmd.Parameters.AddWithValue("@kullaniciId", (object?)yeniSatis.KullaniciId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@uId", yeniSatis.UrunId);
                cmd.Parameters.AddWithValue("@barkod", yeniSatis.BarkodNo);
                cmd.Parameters.AddWithValue("@isim", yeniSatis.Isim);
                cmd.Parameters.AddWithValue("@kategori", yeniSatis.Kategori);
                cmd.Parameters.AddWithValue("@fiyat", yeniSatis.SatisFiyati);
                cmd.Parameters.AddWithValue("@adet", yeniSatis.Adet);
                cmd.Parameters.AddWithValue("@total", yeniSatis.Total);

                // Tarihi artık UI katmanından gelen değerden alıyoruz
                cmd.Parameters.AddWithValue("@tarih", yeniSatis.SatisTarihi);

                // Yeni eklediğimiz Müşteri ID parametresi
                if (yeniSatis.MusteriId.HasValue && yeniSatis.MusteriId.Value > 0)
                {
                    cmd.Parameters.AddWithValue("@musteriId", yeniSatis.MusteriId.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@musteriId", DBNull.Value);
                }

                try
                {
                    baglanti.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Satış kaydedilirken hata: " + ex.Message);
                    return false;
                }
            }
        }
        // SatislarDao sınıfının içine eklenecek metod:
        public DataTable UrunSatisGrafigiGetir(int urunId)
        {
            DataTable dt = new DataTable();

            // Kendi veritabanı bağlantı cümleni buraya yazmalısın
            string connectionString = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // DATE(SatisTarihi) kullanarak saatleri yoksayıyor, sadece gün bazında grupluyoruz.
                string query = "SELECT DATE(SatisTarihi) as Tarih, SUM(Adet) as ToplamAdet FROM satislar WHERE UrunId = @urunId GROUP BY DATE(SatisTarihi) ORDER BY Tarih ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@urunId", urunId);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt); // Gelen verileri DataTable içine dolduruyoruz
                }
            }
            return dt;
        }

    }
}
