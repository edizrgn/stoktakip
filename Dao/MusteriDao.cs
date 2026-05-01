using MySql.Data.MySqlClient;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Dao
{
    public class MusteriDao
    {
        // Veritabanı bağlantı cümlen (Eğer XAMPP'ta root şifren yoksa Pwd boş kalır)
        string connectionString = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd=;";

        // Müşteri Ekleme Metodu
        public void MusteriEkle(Musteri musteri)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO musteriler (ad_soyad, telefon, eposta, adres) VALUES (@ad, @tel, @eposta, @adres)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", musteri.AdSoyad);
                    cmd.Parameters.AddWithValue("@tel", musteri.Telefon);
                    cmd.Parameters.AddWithValue("@eposta", musteri.Eposta);
                    cmd.Parameters.AddWithValue("@adres", musteri.Adres);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Musteri> MusterileriGetir()
        {
            List<Musteri> musteriListesi = new List<Musteri>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, ad_soyad, telefon, eposta, adres FROM musteriler";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Musteri m = new Musteri
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                AdSoyad = reader["ad_soyad"].ToString(),
                                Telefon = reader["telefon"].ToString(),
                                Eposta = reader["eposta"].ToString(),
                                Adres = reader["adres"].ToString()
                            };
                            musteriListesi.Add(m);
                        }
                    }
                }
            }
            return musteriListesi;
        }

        // Müşteri Güncelleme Metodu
        public bool MusteriGuncelle(Musteri musteri)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE musteriler
                                 SET ad_soyad = @ad, telefon = @tel, eposta = @eposta, adres = @adres
                                 WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", musteri.AdSoyad);
                    cmd.Parameters.AddWithValue("@tel", musteri.Telefon);
                    cmd.Parameters.AddWithValue("@eposta", musteri.Eposta);
                    cmd.Parameters.AddWithValue("@adres", musteri.Adres);
                    cmd.Parameters.AddWithValue("@id", musteri.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Müşteri Silme Metodu
        public bool MusteriSil(int musteriId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM musteriler WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", musteriId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
