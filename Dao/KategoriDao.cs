using MySql.Data.MySqlClient;
using StokTakip.Config;
using System;
using System.Data;
using System.Windows.Forms;

namespace StokTakip.Dao
{
    public class KategoriDao
    {
        // 1. Kategorileri Tablo (Grid) için Getirme
        public DataTable TumKategorileriGetir()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    // Kategorileri A'dan Z'ye sıralı getiriyoruz
                    string query = "SELECT Id, KategoriAdi FROM kategoriler ORDER BY KategoriAdi ASC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori Getirme Hatası: " + ex.Message);
            }
            return dt;
        }

        // 2. Kategori adı mevcut mu kontrolü (Büyük/Küçük harf duyarsız)
        public bool KategoriAdiVarMi(string kategoriAdi, int? haricKategoriId = null)
        {
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT COUNT(*)
                                     FROM kategoriler
                                     WHERE LOWER(TRIM(KategoriAdi)) = LOWER(TRIM(@adi))";

                    if (haricKategoriId.HasValue)
                    {
                        query += " AND Id <> @id";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adi", kategoriAdi);

                    if (haricKategoriId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@id", haricKategoriId.Value);
                    }

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori Kontrol Hatası: " + ex.Message);
                return false;
            }
        }

        // 3. Yeni Kategori Ekleme
        public bool KategoriEkle(string kategoriAdi)
        {
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO kategoriler (KategoriAdi) VALUES (@adi)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adi", kategoriAdi);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Bu kategori adı zaten mevcut.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori Ekleme Hatası: " + ex.Message);
                return false;
            }
        }

        // 4. Kategori Güncelleme
        public bool KategoriGuncelle(int id, string yeniAd)
        {
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE kategoriler SET KategoriAdi = @adi WHERE Id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adi", yeniAd);
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Bu kategori adı zaten mevcut.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori Güncelleme Hatası: " + ex.Message);
                return false;
            }
        }

        // 5. Kategori Silme
        public bool KategoriSil(int id)
        {
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM kategoriler WHERE Id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                // Eğer bir kategoriyi silmeye çalışırken hata verirse, muhtemelen o kategoriye ait ürünler vardır
                MessageBox.Show("Kategori Silme Hatası: " + ex.Message);
                return false;
            }
        }
    }
}
