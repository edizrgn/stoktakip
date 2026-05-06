# StokTakip

**StokTakip**, küçük ve orta ölçekli işletmeler için geliştirilen, ürün, stok, satış, müşteri ve raporlama süreçlerini tek panel üzerinden yönetmeyi amaçlayan bir **C# Windows Forms stok ve satış takip otomasyonu** projesidir.

Proje; ürün ekleme, stok artırma/azaltma, satış kaydı oluşturma, müşteri yönetimi, kategori yönetimi, barkod/kamera ile ürün okuma ve satış raporlarını Excel olarak dışa aktarma gibi temel işletme ihtiyaçlarını karşılamaya odaklanır.

---

## Özellikler

- Kullanıcı giriş ve kayıt sistemi
- E-posta doğrulama kodu ile kayıt onayı
- Admin ve Personel rol desteği
- Rol bazlı dashboard görünümü
- Ürün ekleme, güncelleme ve silme
- Ürünlere barkod, kategori, alış fiyatı, satış fiyatı, stok miktarı ve görsel ekleme
- Stok ekleme ve stok düşme işlemleri
- Stok hareketlerinin takip edilmesi
- Satış kaydı oluşturma
- Müşteri yönetimi
- Kategori yönetimi
- Ana sayfada satış, kazanç, kritik stok ve sipariş özetleri
- Ürün/stok dağılımı için grafik gösterimi
- Kamera ile barkod okuma
- Barkod sistemde kayıtlıysa stok ekranına, kayıtlı değilse ürün kayıt ekranına yönlendirme
- Raporları tarih, kategori ve ürüne göre filtreleme
- Satış raporlarını Excel dosyası olarak dışa aktarma

---

## Kullanılan Teknolojiler

- **C#**
- **.NET Windows Forms**
- **MySQL / MariaDB**
- **Guna.UI2.WinForms**
- **MySql.Data**
- **ZXing.Net**
- **AForge.Video.DirectShow**
- **ClosedXML**
- **LiveCharts.WinForms**
- **ScottPlot.WinForms**
- **WinForms.DataVisualization**

---

## Proje Yapısı

```txt
StokTakip/
├── Config/
│   └── DbConfig.cs
├── Dao/
│   ├── KategoriDao.cs
│   ├── KullaniciDao.cs
│   ├── MusteriDao.cs
│   ├── UrunDao.cs
│   └── satislarDao.cs
├── Models/
│   ├── Kategori.cs
│   ├── Kullanici.cs
│   ├── Musteri.cs
│   ├── Oturum.cs
│   ├── Urun.cs
│   └── satislar.cs
├── Presenters/
│   ├── KategoriPresenter.cs
│   ├── ProfilPresenter.cs
│   ├── StokPresenter.cs
│   └── UrunlerPresenter.cs
├── Services/
│   └── EmailServices.cs
├── Views/
│   ├── Form1.cs
│   ├── RegisterForm.cs
│   ├── VerificationForm.cs
│   ├── DashboardForm.cs
│   ├── BarkodOkuyucuForm.cs
│   ├── UC_AnaSayfa.cs
│   ├── UC_Urunler.cs
│   ├── UC_Stok.cs
│   ├── UC_Satis.cs
│   ├── UC_Musteriler.cs
│   ├── UC_Kategoriler.cs
│   ├── UC_Personeller.cs
│   ├── UC_Profil.cs
│   └── UC_Raporlar.cs
├── Program.cs
├── StokTakip.csproj
└── stoktakipdb.sql
```

---

## Veritabanı Yapısı

Proje MySQL/MariaDB veritabanı kullanır. Veritabanı dosyası repoda yer alan:

```txt
stoktakipdb.sql
```

dosyasıdır.

Bu dosya içerisinde aşağıdaki temel tablolar bulunur:

- `kullanicilar`
- `urunler`
- `kategoriler`
- `satislar`
- `musteriler`
- `stok_hareketleri`

Ayrıca ürün eklendiğinde veya ürün stoğu değiştiğinde stok hareketi kaydı oluşturmak için tetikleyiciler de kullanılmıştır.

## UML ve ER Diagramları

<img width="3900" height="3350" alt="UML_Diagram" src="https://github.com/user-attachments/assets/69a34bcc-2af2-455f-a269-abef2175ce28" />
<img width="1575" height="998" alt="ER_Diagram" src="https://github.com/user-attachments/assets/61864706-cab9-4e44-915c-02b944a3ec2d" />


---

## Kurulum

### 1. Repoyu klonlayın

```bash
git clone https://github.com/edizrgn/stoktakip.git
cd stoktakip
```

### 2. Projeyi Visual Studio ile açın

Projeyi Visual Studio üzerinden açın.

> Bu proje Windows Forms kullandığı için Windows ortamında çalıştırılması önerilir.

### 3. NuGet paketlerini yükleyin

Visual Studio projeyi açtığınızda gerekli NuGet paketlerini otomatik olarak geri yükleyebilir.

Gerekirse manuel olarak şu paketlerin yüklü olduğundan emin olun:

- `Guna.UI2.WinForms`
- `MySql.Data`
- `ClosedXML`
- `ZXing.Net`
- `ZXing.Net.Bindings.Windows.Compatibility`
- `AForge.Video.DirectShow`
- `LiveCharts.WinForms`
- `ScottPlot.WinForms`
- `WinForms.DataVisualization`

### 4. Veritabanını içe aktarın

MySQL veya MariaDB üzerinde `stoktakipdb.sql` dosyasını içe aktarın.

Örnek:

```bash
mysql -u root -p < stoktakipdb.sql
```

Ya da phpMyAdmin / HeidiSQL gibi bir araç üzerinden SQL dosyasını çalıştırabilirsiniz.

### 5. Veritabanı bağlantısını düzenleyin

`Config/DbConfig.cs` dosyasındaki bağlantı bilgisini kendi MySQL/MariaDB ayarlarınıza göre düzenleyin:

```csharp
private static string connectionString = "Server=localhost;Database=StokTakipDB;Uid=root;Pwd=;";
```

Örnek:

```csharp
private static string connectionString = "Server=localhost;Database=stoktakipdb;Uid=root;Pwd=sifreniz;";
```

> Güvenlik açısından veritabanı şifresi, SMTP bilgileri gibi hassas verilerin doğrudan kaynak kod içinde tutulmaması önerilir.

---

## E-posta Doğrulama Ayarı

Kayıt işlemi sırasında kullanıcıya doğrulama kodu göndermek için `Services/EmailServices.cs` dosyası kullanılır.

SMTP ayarlarını kendi e-posta sağlayıcınıza göre düzenlemeniz gerekir.

Önerilen yaklaşım:

- SMTP kullanıcı adı ve şifresini doğrudan kod içine yazmayın.
- Ortam değişkeni, kullanıcı gizli dosyası veya güvenli konfigürasyon yöntemi kullanın.
- Test ortamı ile gerçek ortam bilgilerini ayırın.

---

## Kullanım

### Giriş / Kayıt

Uygulama `Form1` giriş ekranı ile başlar.

Kullanıcı:

1. Kayıt olur.
2. E-posta doğrulama kodunu girer.
3. Giriş yaptıktan sonra dashboard ekranına yönlendirilir.

### Dashboard

Dashboard üzerinden şu modüllere erişilebilir:

- Ana Sayfa
- Ürün Yönetimi
- Stok Yönetimi
- Satış Yönetimi
- Müşteri Yönetimi
- Kategori Yönetimi
- Raporlar
- Profil
- Personel Yönetimi

> Personel yönetimi ekranı yalnızca Admin rolündeki kullanıcılara gösterilir.

### Barkod Okuma

Barkod okuma ekranı bilgisayara bağlı kameraları kullanır.

- Barkod sistemde kayıtlıysa stok ekranına yönlendirilir.
- Barkod sistemde kayıtlı değilse ürün kayıt ekranına yönlendirilir ve barkod alanı otomatik doldurulur.

### Raporlama

Raporlar ekranında satışlar:

- Tarihe göre
- Kategoriye göre
- Ürüne göre
- Artan/azalan sıralamaya göre

filtrelenebilir.

Filtrelenen sonuçlar Excel dosyası olarak dışa aktarılabilir.

---

## Geliştirme Notları

Bu proje katmanlı bir yapıya yakın şekilde geliştirilmiştir.

- `Models`: Veritabanı nesnelerini temsil eden sınıflar
- `Dao`: Veritabanı işlemleri
- `Views`: Windows Forms ekranları ve UserControl arayüzleri
- `Presenters`: View ile DAO arasındaki iş mantığı bağlantısı
- `Services`: E-posta gibi yardımcı servisler
- `Config`: Veritabanı bağlantı ayarları

Bu yapı sayesinde arayüz, veri erişimi ve iş mantığı birbirinden daha okunabilir şekilde ayrılmıştır.

---

## Yapılabilecek İyileştirmeler

- Hassas bilgileri kaynak koddan çıkarıp güvenli konfigürasyon yapısına taşımak
- Şifreleri düz metin yerine hash algoritması ile saklamak
- Veritabanı bağlantı cümlelerini tek merkezden yönetmek
- Hata yönetimini daha merkezi hale getirmek
- Loglama sistemi eklemek
- Kullanıcı yetkilendirmesini daha detaylı hale getirmek
- Kurulum için örnek `.env` veya `appsettings` benzeri yapı hazırlamak
- README içine ekran görüntüleri eklemek
- Birim testleri eklemek

---

## Lisans

Bu proje eğitim ve geliştirme amacıyla hazırlanmıştır.

Lisans bilgisi eklemek isterseniz repoya ayrıca bir `LICENSE` dosyası ekleyebilirsiniz.

---

## Geliştiriciler

- [**Ediz Ergin**](https://github.com/edizrgn
- [**Toygar Yıldız**](https://github.com/ToygarYldz)
- [**Mikail Çelik**](https://github.com/mikail4734)
- [**Yiğit Yüce**](http://github.com/ygtyce19)

GitHub: [edizrgn](https://github.com/edizrgn)
