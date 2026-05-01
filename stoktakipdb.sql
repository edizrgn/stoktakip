-- --------------------------------------------------------
-- Sunucu:                       127.0.0.1
-- Sunucu sürümü:                10.4.32-MariaDB - mariadb.org binary distribution
-- Sunucu İşletim Sistemi:       Win64
-- HeidiSQL Sürüm:               12.17.0.7270
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- stoktakipdb için veritabanı yapısı dökülüyor
CREATE DATABASE IF NOT EXISTS `stoktakipdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `stoktakipdb`;

-- tablo yapısı dökülüyor stoktakipdb.kategoriler
CREATE TABLE IF NOT EXISTS `kategoriler` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `KategoriAdi` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UQ_kategoriler_KategoriAdi` (`KategoriAdi`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- stoktakipdb.kategoriler: ~4 rows (yaklaşık) tablosu için veriler indiriliyor
REPLACE INTO `kategoriler` (`Id`, `KategoriAdi`) VALUES
	(5, 'Elektronik'),
	(24, 'Giyim');

-- tablo yapısı dökülüyor stoktakipdb.kullanicilar
CREATE TABLE IF NOT EXISTS `kullanicilar` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AdSoyad` varchar(100) NOT NULL,
  `Eposta` varchar(100) NOT NULL,
  `Sifre` varchar(255) NOT NULL,
  `Rol` enum('Admin','Personel') NOT NULL,
  `Durum` tinyint(1) DEFAULT 1,
  `KayitTarihi` datetime DEFAULT current_timestamp(),
  `ResimYolu` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Eposta` (`Eposta`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- stoktakipdb.kullanicilar: ~3 rows (yaklaşık) tablosu için veriler indiriliyor
REPLACE INTO `kullanicilar` (`Id`, `AdSoyad`, `Eposta`, `Sifre`, `Rol`, `Durum`, `KayitTarihi`, `ResimYolu`) VALUES
	(23, 'Toygar Yıldız', 'toygaryildiz34@gmail.com', '123', 'Admin', 1, '2026-03-11 12:21:55', NULL),
	(24, 'furkan yasar', 'furkanyasar1724@gmail.com', '1624', 'Personel', 0, '2026-03-11 12:44:19', NULL),
	(29, 'mikail çelik', 'mikailcelik4734@gmail.com', '4747', 'Personel', 1, '2026-04-04 15:13:35', 'C:\\Users\\mikai\\OneDrive\\Masaüstü\\stoktakip1 (3) (1) (1)\\stoktakip1 (2) (1) (1)\\stoktakip1 (2) (1)\\stoktakip1 (1)\\stoktakip1\\stoktakip1\\stoktakip1\\bin\\Debug\\net10.0-windows\\PersonelResimleri\\29.jpg');

-- tablo yapısı dökülüyor stoktakipdb.musteriler
CREATE TABLE IF NOT EXISTS `musteriler` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ad_soyad` varchar(100) NOT NULL,
  `telefon` varchar(20) DEFAULT NULL,
  `eposta` varchar(100) DEFAULT NULL,
  `adres` text DEFAULT NULL,
  `kayit_tarihi` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- stoktakipdb.musteriler: ~2 rows (yaklaşık) tablosu için veriler indiriliyor
REPLACE INTO `musteriler` (`id`, `ad_soyad`, `telefon`, `eposta`, `adres`, `kayit_tarihi`) VALUES
	(1, 'mikail çelik', '5414201447', 'mikailcelik47@gmail.com', 'şirinevler', '2026-04-19 11:20:18'),
	(7, 'Ediz Ergin', '5515546814', 'ediz25e@gmail.com', 'Bakırköy/İstanbul', '2026-04-27 22:22:50');

-- tablo yapısı dökülüyor stoktakipdb.satislar
CREATE TABLE IF NOT EXISTS `satislar` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `KullaniciId` int(11) DEFAULT NULL,
  `UrunId` int(11) NOT NULL,
  `BarkodNo` varchar(50) DEFAULT NULL,
  `Isim` varchar(100) DEFAULT NULL,
  `Kategori` varchar(50) DEFAULT NULL,
  `SatisFiyati` decimal(18,2) DEFAULT NULL,
  `Adet` int(11) DEFAULT NULL,
  `Total` decimal(18,2) DEFAULT NULL,
  `SatisTarihi` datetime DEFAULT current_timestamp(),
  `MusteriId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Satis_Kullanici` (`KullaniciId`),
  KEY `FK_Satis_Urun` (`UrunId`),
  KEY `FK_Satis_Musteri` (`MusteriId`),
  CONSTRAINT `FK_Satis_Musteri` FOREIGN KEY (`MusteriId`) REFERENCES `musteriler` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_Satis_Urun` FOREIGN KEY (`UrunId`) REFERENCES `urunler` (`UrunID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=80 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- stoktakipdb.satislar: ~30 rows (yaklaşık) tablosu için veriler indiriliyor
REPLACE INTO `satislar` (`Id`, `KullaniciId`, `UrunId`, `BarkodNo`, `Isim`, `Kategori`, `SatisFiyati`, `Adet`, `Total`, `SatisTarihi`, `MusteriId`) VALUES
	(49, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:02:56', NULL),
	(50, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:06:32', NULL),
	(51, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:08:40', NULL),
	(52, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:20:04', NULL),
	(53, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:23:44', NULL),
	(54, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:26:37', NULL),
	(55, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:30:46', NULL),
	(56, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:35:42', NULL),
	(57, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:36:29', NULL),
	(58, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:39:15', NULL),
	(59, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:43:29', NULL),
	(60, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:49:02', NULL),
	(61, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:54:06', NULL),
	(62, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:56:15', NULL),
	(63, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 20:57:45', NULL),
	(64, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 21:02:41', NULL),
	(65, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 21:19:12', NULL),
	(66, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 21:20:06', NULL),
	(67, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 21:26:35', NULL),
	(68, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 21:43:07', NULL),
	(69, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 2, 50.00, '2026-03-31 21:52:22', NULL),
	(70, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 1, 25.00, '2026-03-31 22:25:35', NULL),
	(71, NULL, 35, '1111111111111', 'bilgisayar', 'Elektronik', 200.00, 6, 1200.00, '2026-04-08 12:12:48', NULL),
	(72, NULL, 35, '1111111111111', 'bilgisayar', 'Elektronik', 200.00, 20, 4000.00, '2026-04-08 12:12:48', NULL),
	(73, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 3, 75.00, '2026-04-19 15:18:43', NULL),
	(74, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 3, 75.00, '2026-04-19 15:19:16', NULL),
	(75, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 2, 50.00, '2026-04-19 15:21:42', NULL),
	(76, NULL, 15, '123456788', 'Sweatshirt', 'Giyim', 25.00, 2, 50.00, '2026-04-19 15:27:20', NULL),
	(77, 23, 24, '223456789', 'Klavye', 'Elektronik', 1500.00, 12, 18000.00, '2026-04-27 23:58:12', NULL),
	(78, 23, 24, '223456789', 'Klavye', 'Elektronik', 1500.00, 29, 43500.00, '2026-04-28 00:26:25', 1),
	(79, 23, 24, '223456789', 'Klavye', 'Elektronik', 1500.00, 25, 37500.00, '2026-04-28 01:59:53', 7);

-- tablo yapısı dökülüyor stoktakipdb.stok_hareketleri
CREATE TABLE IF NOT EXISTS `stok_hareketleri` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UrunID` int(11) DEFAULT NULL,
  `BarkodNo` varchar(50) NOT NULL,
  `IslemTipi` enum('ILK_KAYIT','STOK_EKLE','STOK_DUS','DUZELTME') NOT NULL DEFAULT 'DUZELTME',
  `MiktarDegisim` int(11) NOT NULL,
  `OncekiStok` int(11) DEFAULT NULL,
  `SonrakiStok` int(11) DEFAULT NULL,
  `IslemTarihi` datetime NOT NULL DEFAULT current_timestamp(),
  `Aciklama` varchar(255) DEFAULT NULL,
  `KullaniciID` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `idx_stok_hareketleri_barkod_tarih` (`BarkodNo`,`IslemTarihi`),
  KEY `idx_stok_hareketleri_urun` (`UrunID`),
  KEY `idx_stok_hareketleri_kullanici` (`KullaniciID`),
  CONSTRAINT `FK_StokHareket_Kullanici` FOREIGN KEY (`KullaniciID`) REFERENCES `kullanicilar` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_StokHareket_Urun` FOREIGN KEY (`UrunID`) REFERENCES `urunler` (`UrunID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- stoktakipdb.stok_hareketleri: ~3 rows (yaklaşık) tablosu için veriler indiriliyor
REPLACE INTO `stok_hareketleri` (`Id`, `UrunID`, `BarkodNo`, `IslemTipi`, `MiktarDegisim`, `OncekiStok`, `SonrakiStok`, `IslemTarihi`, `Aciklama`, `KullaniciID`) VALUES
	(1, 15, '123456788', 'ILK_KAYIT', 75, 0, 75, '2026-03-17 05:17:43', 'Ürün ilk kez eklendi', 24),
	(2, 15, '123456788', 'STOK_DUS', -5, 75, 70, '2026-03-31 20:57:45', 'Satış/çıkış sonrası düşüş', 24),
	(3, 15, '123456788', 'STOK_DUS', -3, 70, 67, '2026-04-01 11:35:12', 'Satış/çıkış sonrası düşüş', 24),
	(4, 15, '123456788', 'STOK_EKLE', 10, 67, 77, '2026-04-05 09:22:08', 'Stok takviyesi', 24),
	(5, 15, '123456788', 'STOK_DUS', -15, 77, 62, '2026-04-10 17:44:20', 'Satış/çıkış sonrası düşüş', 24),
	(6, 15, '123456788', 'STOK_DUS', -10, 62, 52, '2026-04-19 15:27:20', 'Satış/çıkış sonrası düşüş', 24),
	(7, 35, '1111111111111', 'ILK_KAYIT', 100, 0, 100, '2026-04-08 12:12:14', 'Ürün ilk kez eklendi', 23),
	(8, 35, '1111111111111', 'STOK_DUS', -6, 100, 94, '2026-04-08 12:12:48', 'Satış sonrası düşüş', 23),
	(9, 35, '1111111111111', 'STOK_DUS', -20, 94, 74, '2026-04-08 12:12:48', 'Satış sonrası düşüş', 23),
	(10, 17, '123456789', 'ILK_KAYIT', 95, 0, 95, '2026-03-17 05:19:01', 'Ürün ilk kez eklendi', 23),
	(11, 24, '223456789', 'ILK_KAYIT', 149, 0, 149, '2026-03-18 17:14:52', 'Ürün ilk kez eklendi', 23),
	(12, 29, '234567789', 'ILK_KAYIT', 5, 0, 5, '2026-03-18 17:18:13', 'Ürün ilk kez eklendi', 23),
	(13, 22, '123456', 'ILK_KAYIT', 1000, 0, 1000, '2026-03-18 11:15:45', 'Backfill: mevcut stoktan oluşturuldu', 24),
	(14, 25, '233456789', 'ILK_KAYIT', 1, 0, 1, '2026-03-18 17:15:53', 'Backfill: mevcut stoktan oluşturuldu', 23),
	(15, 26, '234456789', 'ILK_KAYIT', 3, 0, 3, '2026-03-18 17:16:32', 'Backfill: mevcut stoktan oluşturuldu', 23),
	(16, 24, '223456789', 'STOK_DUS', -12, 149, 137, '2026-04-27 23:58:12', 'Trigger: stok azaltıldı', 23),
	(17, 24, '223456789', 'STOK_DUS', -12, 149, 137, '2026-04-27 23:58:12', 'Uygulama: stok hareketi', 23),
	(18, 24, '223456789', 'STOK_DUS', -29, 137, 108, '2026-04-28 00:26:25', 'Trigger: stok azaltıldı', 23),
	(19, 24, '223456789', 'STOK_DUS', -29, 137, 108, '2026-04-28 00:26:25', 'Uygulama: stok hareketi', 23),
	(20, 24, '223456789', 'STOK_DUS', -25, 108, 83, '2026-04-28 01:59:53', 'Trigger: stok azaltıldı', 23),
	(21, 24, '223456789', 'STOK_DUS', -25, 108, 83, '2026-04-28 01:59:53', 'Uygulama: stok hareketi', 23),
	(22, NULL, '1231123123', 'ILK_KAYIT', 25, 0, 25, '2026-04-28 02:08:23', 'Trigger: ürün ilk eklendi', 23);

-- tablo yapısı dökülüyor stoktakipdb.urunler
CREATE TABLE IF NOT EXISTS `urunler` (
  `UrunID` int(11) NOT NULL AUTO_INCREMENT,
  `BarkodNo` varchar(50) NOT NULL,
  `UrunAdi` varchar(100) NOT NULL,
  `Kategori` varchar(50) NOT NULL,
  `AlisFiyati` decimal(18,2) NOT NULL,
  `SatisFiyati` decimal(18,2) NOT NULL,
  `StokMiktari` int(11) NOT NULL,
  `EklenmeTarihi` datetime DEFAULT current_timestamp(),
  `AktifMi` tinyint(1) DEFAULT 1,
  `ResimYolu` varchar(255) DEFAULT NULL,
  `KullaniciID` int(11) NOT NULL,
  PRIMARY KEY (`UrunID`),
  UNIQUE KEY `BarkodNo` (`BarkodNo`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- stoktakipdb.urunler: ~8 rows (yaklaşık) tablosu için veriler indiriliyor
REPLACE INTO `urunler` (`UrunID`, `BarkodNo`, `UrunAdi`, `Kategori`, `AlisFiyati`, `SatisFiyati`, `StokMiktari`, `EklenmeTarihi`, `AktifMi`, `ResimYolu`, `KullaniciID`) VALUES
	(15, '123456788', 'Sweatshirt', 'Giyim', 15.00, 25.00, 52, '2026-03-17 05:17:43', 1, 'C:\\Users\\TOYGAR\\OneDrive\\Masaüstü\\images.jpg', 24),
	(17, '123456789', 'Mouse', 'Elektronik', 500.00, 750.00, 95, '2026-03-17 05:19:01', 1, 'C:\\Users\\TOYGAR\\OneDrive\\Masaüstü\\mouse.jpg', 23),
	(22, '123456', 'fare', 'teknojia', 5.00, 100.00, 1000, '2026-03-18 11:15:45', 1, 'C:\\Users\\TOYGAR\\OneDrive\\Masaüstü\\mouse.jpg', 24),
	(24, '223456789', 'Klavye', 'Elektronik', 1000.00, 1500.00, 83, '2026-03-18 17:14:52', 1, 'C:\\Users\\TOYGAR\\OneDrive\\Masaüstü\\images (1).jpg', 23),
	(25, '233456789', 'Acer Nitro', 'Elektronik', 30000.00, 35000.00, 1, '2026-03-18 17:15:53', 1, 'C:\\Users\\TOYGAR\\OneDrive\\Masaüstü\\acernitro.jpg', 23),
	(26, '234456789', 'Asus Rog', 'Elektronik', 35000.00, 40000.00, 3, '2026-03-18 17:16:32', 1, 'C:\\Users\\TOYGAR\\OneDrive\\Masaüstü\\asusrog.png', 23),
	(29, '234567789', 'Msi', 'Elektronik', 40000.00, 45000.00, 5, '2026-03-18 17:18:13', 1, 'C:\\Users\\mikai\\OneDrive\\Resimler\\133803237734161866.jpg', 23),
	(35, '1111111111111', 'bilgisayar', 'Elektronik', 100.00, 200.00, 74, '2026-04-08 12:12:14', 1, NULL, 23);

-- tetikleyici yapısı dökülüyor stoktakipdb.trg_urunler_ai_stok_hareketi
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO';
DELIMITER //
CREATE TRIGGER `trg_urunler_ai_stok_hareketi`
AFTER INSERT ON `urunler`
FOR EACH ROW
BEGIN
  IF IFNULL(NEW.`StokMiktari`, 0) <> 0 THEN
    INSERT INTO `stok_hareketleri`
    (`UrunID`, `BarkodNo`, `IslemTipi`, `MiktarDegisim`, `OncekiStok`, `SonrakiStok`, `IslemTarihi`, `Aciklama`, `KullaniciID`)
    VALUES
    (NEW.`UrunID`, NEW.`BarkodNo`, 'ILK_KAYIT', NEW.`StokMiktari`, 0, NEW.`StokMiktari`, NOW(), 'Trigger: ürün ilk eklendi', NEW.`KullaniciID`);
  END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- tetikleyici yapısı dökülüyor stoktakipdb.trg_urunler_au_stok_hareketi
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO';
DELIMITER //
CREATE TRIGGER `trg_urunler_au_stok_hareketi`
AFTER UPDATE ON `urunler`
FOR EACH ROW
BEGIN
  DECLARE fark INT DEFAULT 0;
  SET fark = IFNULL(NEW.`StokMiktari`, 0) - IFNULL(OLD.`StokMiktari`, 0);

  IF fark <> 0 THEN
    INSERT INTO `stok_hareketleri`
    (`UrunID`, `BarkodNo`, `IslemTipi`, `MiktarDegisim`, `OncekiStok`, `SonrakiStok`, `IslemTarihi`, `Aciklama`, `KullaniciID`)
    VALUES
    (
      NEW.`UrunID`,
      NEW.`BarkodNo`,
      IF(fark > 0, 'STOK_EKLE', 'STOK_DUS'),
      fark,
      OLD.`StokMiktari`,
      NEW.`StokMiktari`,
      NOW(),
      CONCAT('Trigger: stok ', IF(fark > 0, 'artırıldı', 'azaltıldı')),
      NEW.`KullaniciID`
    );
  END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
