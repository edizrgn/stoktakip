using MySql.Data.MySqlClient; // MySQL kütüphanen ekli olmalı
using StokTakip.Config;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StokTakip.Dao
{
    public class UrunDao
    {
        // Bağlantı dizesini Login sayfasındakiyle aynı yapmalısın!
        private string connectionString = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd='';";


        // Okunan barkodun sistemde kayıtlı olup olmadığını kontrol eder
        public bool BarkodMevcutMu(string barkod)
        {
            try
            {
                using (var conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM urunler WHERE BarkodNo = @barkod";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@barkod", barkod.Trim());

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Eğer 1 veya daha fazlaysa TRUE (Kayıtlı) döner
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Barkod Kontrol Hatası: " + ex.Message);
                return false;
            }
        }


        // 1. ÜRÜN EKLEME (CREATE)
        // UrunDao.cs - UrunEkle Metodu
        public bool UrunEkle(Urun urun)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Sorguya 'KullaniciID' sütununu ve '@kullaniciId' parametresini ekledik
                    string query = "INSERT INTO Urunler (BarkodNo, UrunAdi, Kategori, AlisFiyati, SatisFiyati, StokMiktari, ResimYolu, KullaniciID) " +
                                   "VALUES (@barkod, @ad, @kat, @alis, @satis, @stok, @resim, @kullaniciId)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@barkod", urun.BarkodNo);
                    cmd.Parameters.AddWithValue("@ad", urun.UrunAdi);
                    cmd.Parameters.AddWithValue("@kat", urun.Kategori);
                    cmd.Parameters.AddWithValue("@alis", urun.AlisFiyati);
                    cmd.Parameters.AddWithValue("@satis", urun.SatisFiyati);
                    cmd.Parameters.AddWithValue("@stok", urun.StokMiktari);
                    cmd.Parameters.AddWithValue("@resim", (object)urun.ResimYolu ?? DBNull.Value);

                    // İŞTE BURASI: Urun modelinden gelen KullaniciID'yi veritabanına gönderiyoruz
                    cmd.Parameters.AddWithValue("@kullaniciId", urun.KullaniciID);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        // 2. TÜM ÜRÜNLERİ GETİRME (READ)
        public DataTable TumUrunleriGetir()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Urunler WHERE AktifMi = 1 AND KullaniciID = @kullaniciId ORDER BY UrunID DESC";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                adapter.Fill(dt);
            }
            return dt;
        }

        // 3. ÜRÜN GÜNCELLEME (UPDATE)
        public bool UrunGuncelle(Urun urun)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    int urunId = 0;
                    int oncekiStok = 0;

                    string secQuery = @"SELECT UrunID, StokMiktari
                                        FROM Urunler
                                        WHERE BarkodNo = @barkod AND KullaniciID = @kullaniciId
                                        LIMIT 1
                                        FOR UPDATE";

                    using (MySqlCommand secCmd = new MySqlCommand(secQuery, conn, transaction))
                    {
                        secCmd.Parameters.AddWithValue("@barkod", urun.BarkodNo);
                        secCmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                        using (var reader = secCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                transaction.Rollback();
                                return false;
                            }

                            urunId = Convert.ToInt32(reader["UrunID"]);
                            oncekiStok = Convert.ToInt32(reader["StokMiktari"]);
                        }
                    }

                    // WHERE kısmına 'AND KullaniciID = @kullaniciId' ekledik!
                    string query = @"UPDATE Urunler
                                     SET UrunAdi=@ad, Kategori=@kat, AlisFiyati=@alis, SatisFiyati=@satis,
                                         StokMiktari=@stok, ResimYolu=@resim
                                     WHERE UrunID=@urunId AND KullaniciID=@kullaniciId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ad", urun.UrunAdi);
                        cmd.Parameters.AddWithValue("@kat", urun.Kategori);
                        cmd.Parameters.AddWithValue("@alis", urun.AlisFiyati);
                        cmd.Parameters.AddWithValue("@satis", urun.SatisFiyati);
                        cmd.Parameters.AddWithValue("@stok", urun.StokMiktari);
                        cmd.Parameters.AddWithValue("@resim", (object)urun.ResimYolu ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@urunId", urunId);
                        cmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                        if (cmd.ExecuteNonQuery() <= 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    if (oncekiStok != urun.StokMiktari)
                    {
                        int fark = urun.StokMiktari - oncekiStok;
                        StokHareketKaydiEkle(
                            conn,
                            transaction,
                            urunId,
                            urun.BarkodNo,
                            fark,
                            oncekiStok,
                            urun.StokMiktari,
                            fark > 0 ? "STOK_EKLE" : "STOK_DUS");
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    if (transaction != null)
                    {
                        try { transaction.Rollback(); } catch { }
                    }

                    return false;
                }
            }
        }

        // 4. ÜRÜN SİLME (DELETE)
        public bool UrunSil(string barkod)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Sadece bu barkoda SAHİP ve bu KULLANICIYA ait ürünü sil
                    string query = "DELETE FROM Urunler WHERE BarkodNo=@barkod AND KullaniciID=@kullaniciId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@barkod", barkod);
                    cmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        // UrunDao.cs içine eklenecek
        public DataTable KategoriBazliUrunDagilimiGetir()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Kategorileri grupla ve her gruptaki ürün sayısını (UrunSayisi) bul
                    string query = "SELECT Kategori, COUNT(*) AS UrunSayisi FROM Urunler WHERE AktifMi = 1 AND KullaniciID = @kullaniciId GROUP BY Kategori";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                    adapter.Fill(dt);
                }
                catch { /* Hata yönetimi eklenebilir */ }
            }
            return dt;
        }

        // GRAFİK İÇİN: Tüm aktif ürünleri stok adetleriyle getirir
        public DataTable UrunBazliStokDagilimiGetir()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT UrunAdi, StokMiktari
                                     FROM Urunler
                                     WHERE AktifMi = 1 AND KullaniciID = @kullaniciId
                                     ORDER BY StokMiktari DESC, UrunAdi ASC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                    adapter.Fill(dt);
                }
                catch
                {
                    // Öğrenci projesi için basit bırakıyoruz; üst katmanda mesaj veriliyor.
                }
            }
            return dt;
        }

        // Ürün bazında satış geçmişini (günlük adet toplamı) getirir.
        public DataTable UrunSatisAdetGecmisiGetir(string barkodNo)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT x.Tarih, x.ToplamSatisAdedi
                                     FROM (
                                         SELECT DATE(SatisTarihi) AS Tarih, SUM(Adet) AS ToplamSatisAdedi
                                         FROM satislar
                                         WHERE BarkodNo = @barkod
                                           AND (KullaniciId = @kullaniciId OR KullaniciId IS NULL)
                                         GROUP BY DATE(SatisTarihi)
                                         ORDER BY Tarih DESC
                                         LIMIT 15
                                     ) x
                                     ORDER BY x.Tarih ASC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@barkod", barkodNo);
                    adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                    adapter.Fill(dt);
                }
                catch
                {
                    // Tabloda/kolonda değişiklik varsa UI tarafında "Veri Yok" gösterilecek.
                }
            }
            return dt;
        }

        // Ürün bazında stok hareket geçmişini (günlük miktar değişimi) getirir.
        // Beklenen tablo: stok_hareketleri(BarkodNo, IslemTarihi, MiktarDegisim)
        public DataTable UrunStokHareketGecmisiGetir(string barkodNo)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (!TabloVarMi(conn, null, "stok_hareketleri"))
                    {
                        return dt;
                    }

                    bool kullaniciKolonuVar = KolonVarMi(conn, null, "stok_hareketleri", "KullaniciID");

                    // Gün bazında birleştirmek yerine son hareketleri tek tek getiriyoruz.
                    // Böylece yeni eklenen işlem yeni bir sütun olarak hemen görünür.
                    string query = @"SELECT x.Tarih, x.ToplamStokDegisimi
                                     FROM (
                                         SELECT IslemTarihi AS Tarih, MiktarDegisim AS ToplamStokDegisimi
                                         FROM stok_hareketleri
                                         WHERE BarkodNo = @barkod " +
                                     (kullaniciKolonuVar ? "AND (KullaniciID = @kullaniciId OR KullaniciID IS NULL) " : "") +
                                     @"ORDER BY IslemTarihi DESC
                                         LIMIT 20
                                     ) x
                                     ORDER BY x.Tarih ASC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@barkod", barkodNo);
                    if (kullaniciKolonuVar)
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                    }
                    adapter.Fill(dt);
                }
                catch
                {
                    // Tabloda/kolonda değişiklik varsa UI tarafında "Veri Yok" gösterilecek.
                }
            }
            return dt;
        }

        // --- STOK SAYFASI İÇİN YENİ METOT ---
        public bool StokMiktariniDegistir(string barkod, int miktar, bool eklenecekMi)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    int urunId = 0;
                    int oncekiStok = 0;

                    // Satırı kilitleyerek mevcut stok değerini çekiyoruz.
                    string secQuery = @"SELECT UrunID, StokMiktari
                                        FROM Urunler
                                        WHERE BarkodNo = @barkod AND KullaniciID = @kullaniciId
                                        LIMIT 1
                                        FOR UPDATE";

                    using (MySqlCommand secCmd = new MySqlCommand(secQuery, conn, transaction))
                    {
                        secCmd.Parameters.AddWithValue("@barkod", barkod);
                        secCmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                        using (var reader = secCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                transaction.Rollback();
                                return false;
                            }

                            urunId = Convert.ToInt32(reader["UrunID"]);
                            oncekiStok = Convert.ToInt32(reader["StokMiktari"]);
                        }
                    }

                    int fark = eklenecekMi ? miktar : -miktar;
                    int sonrakiStok = oncekiStok + fark;

                    if (sonrakiStok < 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    string guncelleQuery = @"UPDATE Urunler
                                             SET StokMiktari = @sonrakiStok
                                             WHERE UrunID = @urunId AND KullaniciID = @kullaniciId";

                    using (MySqlCommand guncelleCmd = new MySqlCommand(guncelleQuery, conn, transaction))
                    {
                        guncelleCmd.Parameters.AddWithValue("@sonrakiStok", sonrakiStok);
                        guncelleCmd.Parameters.AddWithValue("@urunId", urunId);
                        guncelleCmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                        if (guncelleCmd.ExecuteNonQuery() <= 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    StokHareketKaydiEkle(
                        conn,
                        transaction,
                        urunId,
                        barkod,
                        fark,
                        oncekiStok,
                        sonrakiStok,
                        eklenecekMi ? "STOK_EKLE" : "STOK_DUS");

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    if (transaction != null)
                    {
                        try { transaction.Rollback(); } catch { }
                    }

                    return false;
                }
            }
        }

        private void StokHareketKaydiEkle(
            MySqlConnection conn,
            MySqlTransaction transaction,
            int urunId,
            string barkod,
            int miktarDegisim,
            int oncekiStok,
            int sonrakiStok,
            string islemTipi)
        {
            if (!TabloVarMi(conn, transaction, "stok_hareketleri"))
            {
                return;
            }

            bool urunIdKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "UrunID");
            bool barkodKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "BarkodNo");
            bool islemTipiKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "IslemTipi");
            bool miktarKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "MiktarDegisim");
            bool oncekiKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "OncekiStok");
            bool sonrakiKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "SonrakiStok");
            bool tarihKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "IslemTarihi");
            bool aciklamaKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "Aciklama");
            bool kullaniciKolonVar = KolonVarMi(conn, transaction, "stok_hareketleri", "KullaniciID");

            // Miktar ve tarih yoksa grafik için anlamlı kayıt üretilemez.
            if (!miktarKolonVar || !tarihKolonVar)
            {
                return;
            }

            List<string> kolonlar = new List<string>();
            List<string> degerler = new List<string>();

            if (urunIdKolonVar) { kolonlar.Add("UrunID"); degerler.Add("@urunId"); }
            if (barkodKolonVar) { kolonlar.Add("BarkodNo"); degerler.Add("@barkod"); }
            if (islemTipiKolonVar) { kolonlar.Add("IslemTipi"); degerler.Add("@islemTipi"); }
            if (miktarKolonVar) { kolonlar.Add("MiktarDegisim"); degerler.Add("@miktarDegisim"); }
            if (oncekiKolonVar) { kolonlar.Add("OncekiStok"); degerler.Add("@oncekiStok"); }
            if (sonrakiKolonVar) { kolonlar.Add("SonrakiStok"); degerler.Add("@sonrakiStok"); }
            if (tarihKolonVar) { kolonlar.Add("IslemTarihi"); degerler.Add("@islemTarihi"); }
            if (aciklamaKolonVar) { kolonlar.Add("Aciklama"); degerler.Add("@aciklama"); }
            if (kullaniciKolonVar) { kolonlar.Add("KullaniciID"); degerler.Add("@kullaniciId"); }

            string query = $"INSERT INTO stok_hareketleri ({string.Join(", ", kolonlar)}) VALUES ({string.Join(", ", degerler)})";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                if (urunIdKolonVar) cmd.Parameters.AddWithValue("@urunId", urunId);
                if (barkodKolonVar) cmd.Parameters.AddWithValue("@barkod", barkod);
                if (islemTipiKolonVar) cmd.Parameters.AddWithValue("@islemTipi", islemTipi);
                if (miktarKolonVar) cmd.Parameters.AddWithValue("@miktarDegisim", miktarDegisim);
                if (oncekiKolonVar) cmd.Parameters.AddWithValue("@oncekiStok", oncekiStok);
                if (sonrakiKolonVar) cmd.Parameters.AddWithValue("@sonrakiStok", sonrakiStok);
                if (tarihKolonVar) cmd.Parameters.AddWithValue("@islemTarihi", DateTime.Now);
                if (aciklamaKolonVar) cmd.Parameters.AddWithValue("@aciklama", "Uygulama: stok hareketi");
                if (kullaniciKolonVar) cmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                cmd.ExecuteNonQuery();
            }
        }

        private bool TabloVarMi(MySqlConnection conn, MySqlTransaction transaction, string tabloAdi)
        {
            string query = @"SELECT COUNT(*)
                             FROM information_schema.TABLES
                             WHERE TABLE_SCHEMA = DATABASE()
                               AND TABLE_NAME = @tabloAdi";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@tabloAdi", tabloAdi);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private bool KolonVarMi(MySqlConnection conn, MySqlTransaction transaction, string tabloAdi, string kolonAdi)
        {
            string query = @"SELECT COUNT(*)
                             FROM information_schema.COLUMNS
                             WHERE TABLE_SCHEMA = DATABASE()
                               AND TABLE_NAME = @tabloAdi
                               AND COLUMN_NAME = @kolonAdi";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@tabloAdi", tabloAdi);
                cmd.Parameters.AddWithValue("@kolonAdi", kolonAdi);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        public int KritikStokSayisiGetir()
        {
            using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Bu kullanıcıya ait ve stoğu 5'in altında olan ürünleri say (Sen istersen 5 yerine 10 da yapabilirsin)
                    string query = "SELECT COUNT(*) FROM Urunler WHERE StokMiktari <= 5 AND KullaniciID = @kullaniciId";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch { return 0; }
            }
        }
        // GRAFİK İÇİN: En çok satılan 5 ürünü getirir
        public DataTable EnCokSatilanUrunleriGetir()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // satislar tablosundaki fiziksel kolon adı "Isim".
                // Uygulama tarafında standart isim kullanımı için "UrunAdi" alias'ı veriyoruz.
                string query = @"SELECT Isim AS UrunAdi, SUM(Adet) as Toplam
                                 FROM satislar
                                 WHERE KullaniciID = @kullaniciId
                                 GROUP BY Isim
                                 ORDER BY Toplam DESC
                                 LIMIT 5";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                adapter.Fill(dt);
            }
            return dt;
        }

        // TABLO İÇİN: Yapılan son 5 satışı getirir
        public DataTable SonBesSatisiGetir()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"SELECT Id, SatisTarihi, Isim AS UrunAdi, Adet
                                 FROM satislar
                                 WHERE KullaniciID = @kullaniciId
                                 ORDER BY Id DESC
                                 LIMIT 5";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@kullaniciId", Oturum.KullaniciID);
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
