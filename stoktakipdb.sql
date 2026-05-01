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
