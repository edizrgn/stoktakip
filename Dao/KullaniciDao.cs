using MySql.Data.MySqlClient;
using StokTakip.Config;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms; // MessageBox kullanabilmek için eklendi

namespace StokTakip.Dao
{
    public class KullaniciDao
    {
        // Kendi şifrene ve veritabanı ismine göre düzenle
        string connectionString = "Server=localhost;Database=StokTakipDB;Uid=root;Pwd=1234;";

        public Kullanici GirisKontrol(string mail, string pass)
        {
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    // Sorgu: Mail ve şifre tutuyor mu? Hesap aktif mi (Durum=1)?
                    string query = "SELECT * FROM Kullanicilar WHERE Eposta=@mail AND Sifre=@pass AND Durum=1";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Kullanici
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                AdSoyad = reader["AdSoyad"].ToString(),
                                Rol = reader["Rol"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
            return null; // Kullanıcı bulunamadıysa boş döner
        }

        public bool KayitEkle(Kullanici user)
        {
            try
            {
                using (var conn = StokTakip.Config.DbConfig.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Kullanicilar (AdSoyad, Eposta, Sifre, Rol, Durum) VALUES (@ad, @mail, @pass, @rol, @durum)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ad", user.AdSoyad);
                    cmd.Parameters.AddWithValue("@mail", user.Eposta);
                    cmd.Parameters.AddWithValue("@pass", user.Sifre);
                    cmd.Parameters.AddWithValue("@rol", user.Rol);
                    cmd.Parameters.AddWithValue("@durum", user.Durum ? 1 : 0);

                    int etkilenenSatir = cmd.ExecuteNonQuery();
                    return etkilenenSatir > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt Ekleme SQL Hatası: " + ex.Message);
                return false;
            }
        }

        // --- 1. KULLANICI BİLGİLERİNİ GETİREN METOT (DÜZELTİLDİ) ---
        // --- 1. KULLANICI BİLGİLERİNİ GETİREN METOT ---
        public System.Data.DataTable KullaniciBilgileriniGetir(int kullaniciId)
        {
            // SAHTE ŞİFRELİ BAĞLANTIYI SİLDİK, SENİN KENDİ BAĞLANTINI EKLEDİK:
            using (var conn = StokTakip.Config.DbConfig.GetConnection())
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                try
                {
                    conn.Open();
                    string query = "SELECT AdSoyad, Sifre FROM Kullanicilar WHERE Id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", kullaniciId);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bilgi Getirme SQL Hatası: " + ex.Message);
                }
                return dt;
            }
        }

        // --- 2. KULLANICI BİLGİLERİNİ GÜNCELLEYEN METOT ---
        public bool ProfilGuncelle(int kullaniciId, string adSoyad, string yeniSifre)
        {
            // BURAYI DA SENİN KENDİ BAĞLANTINLA DEĞİŞTİRDİK:
            using (var conn = StokTakip.Config.DbConfig.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query;

                    if (string.IsNullOrEmpty(yeniSifre))
                    {
                        query = "UPDATE Kullanicilar SET AdSoyad = @adsoyad WHERE Id = @id";
                    }
                    else
                    {
                        query = "UPDATE Kullanicilar SET AdSoyad = @adsoyad, Sifre = @sifre WHERE Id = @id";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adsoyad", adSoyad);
                    cmd.Parameters.AddWithValue("@id", kullaniciId);

                    if (!string.IsNullOrEmpty(yeniSifre))
                    {
                        cmd.Parameters.AddWithValue("@sifre", yeniSifre);
                    }

                    int etkilenenSatir = cmd.ExecuteNonQuery();
                    return etkilenenSatir > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme SQL Hatası: " + ex.Message);
                    return false;
                }
            }
        }

        public bool PersonelBilgileriniGuncelle(int id, string adSoyad, string eposta, string sifre, int durum)
        {
            using (var conn = StokTakip.Config.DbConfig.GetConnection())
            {
                try
                {
                    conn.Open();
                    // Tüm alanları güncelleyen yeni sorgu
                    string query = "UPDATE Kullanicilar SET AdSoyad=@ad, Eposta=@mail, Sifre=@pass, Durum=@durum WHERE Id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ad", adSoyad);
                    cmd.Parameters.AddWithValue("@mail", eposta);
                    cmd.Parameters.AddWithValue("@pass", sifre);
                    cmd.Parameters.AddWithValue("@durum", durum);
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Personel Güncelleme Hatası: " + ex.Message);
                    return false;
                }
            }
        }
        public System.Data.DataTable TumKullanicilariListele()
        {
            using (var conn = StokTakip.Config.DbConfig.GetConnection())
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                try
                {
                    conn.Open();
                    // Veritabanındaki tüm kullanıcıları çeken sorgu
                    // query satırını şu şekilde güncelle:
                    string query = "SELECT Id, AdSoyad, Eposta, Sifre, Rol, ResimYolu, Durum, KayitTarihi FROM Kullanicilar WHERE Rol != 'Admin'";
                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Liste Getirme Hatası: " + ex.Message);
                }
                return dt;
            }
        }

    

        public bool ResimYoluGuncelle(int id, string hedefYol)
        {
            try
            {
                using (var conn = StokTakip.Config.DbConfig.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Kullanicilar SET ResimYolu = @yol WHERE Id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@yol", hedefYol);
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim Yolu Güncelleme Hatası: " + ex.Message);
                return false;
            }
        }

        public System.Data.DataTable KullaniciAra(string arananKelime)
        {
            using (var conn = StokTakip.Config.DbConfig.GetConnection())
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                try
                {
                    conn.Open();
                    // AdSoyad veya Eposta içinde aranan kelime geçiyor mu?
                    string query = "SELECT Id, AdSoyad, Eposta, Rol, Durum, KayitTarihi FROM Kullanicilar " +
                                   "WHERE AdSoyad LIKE @ara OR Eposta LIKE @ara";

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ara", "%" + arananKelime + "%");

                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Arama Hatası: " + ex.Message);
                }
                return dt;
            }
        }
        public bool DurumGuncelle(int id, int yeniDurum)
        {
            // StokTakip.Config içindeki bağlantı yapını kullanıyoruz
            using (var conn = StokTakip.Config.DbConfig.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sorgu = "UPDATE kullanicilar SET Durum = @durum WHERE Id = @id";

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sorgu, conn);

                    // Parametreleri güvenli şekilde ekliyoruz
                    cmd.Parameters.AddWithValue("@durum", yeniDurum);
                    cmd.Parameters.AddWithValue("@id", id);

                    // Sorguyu çalıştırıyoruz (Etkilenen satır sayısı 0'dan büyükse başarılıdır)
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Veritabanı hatası: " + ex.Message);
                    return false;
                }
            }
        }
    }
}