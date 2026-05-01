using AForge.Video;
using AForge.Video.DirectShow;
using StokTakip.Dao; // UrunDao'yu kullanabilmek için (Senin yolun farklıysa düzelt)
using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;

namespace StokTakip
{
    public partial class BarkodOkuyucuForm : Form
    {
        FilterInfoCollection videoDevices; // Bilgisayardaki kameraların listesi
        VideoCaptureDevice videoSource;    // Kullanacağımız kamera

        // Bu iki bilgiyi ana sayfaya (Dashboard) göndereceğiz
        public string OkunanBarkod { get; private set; }
        public bool UrunKayitliMi { get; private set; }

        public BarkodOkuyucuForm()
        {
            InitializeComponent();
        }

        private void BarkodOkuyucuForm_Paint(object sender, PaintEventArgs e)
        {
            // Ekranın ortasında barkodu tutacağımız alanı belirliyoruz
            Rectangle taramaAlani = new Rectangle(this.Width / 4, this.Height / 4, this.Width / 2, this.Height / 2);

            // 1. Kenarları Karartma (Şeffaf Siyah Katman)
            using (Region r = new Region(this.ClientRectangle))
            {
                r.Exclude(taramaAlani);
                using (SolidBrush firca = new SolidBrush(Color.FromArgb(150, 0, 0, 0))) // %60 şeffaf siyah
                {
                    e.Graphics.FillRegion(firca, r);
                }
            }

            // 2. Neon Yeşil Çerçeve Çizimi
            using (Pen neonKalem = new Pen(Color.LimeGreen, 4))
            {
                e.Graphics.DrawRectangle(neonKalem, taramaAlani);

                // Köşelere estetik çizgiler (Opsiyonel)
                int uzunluk = 40;
                // Sol Üst
                e.Graphics.DrawLine(neonKalem, taramaAlani.X, taramaAlani.Y, taramaAlani.X + uzunluk, taramaAlani.Y);
                e.Graphics.DrawLine(neonKalem, taramaAlani.X, taramaAlani.Y, taramaAlani.X, taramaAlani.Y + uzunluk);
                // Diğer köşeleri de benzer şekilde ekleyebilirsin...
            }
        }
        private void BarkodOkuyucuForm_Load(object sender, EventArgs e)
        {
            // 1. Bilgisayara bağlı kameraları bul ve ComboBox'a doldur
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Bilgisayarınıza bağlı bir kamera bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            foreach (FilterInfo device in videoDevices)
            {
                cmb_Kameralar.Items.Add(device.Name);
            }
            cmb_Kameralar.SelectedIndex = 0;

            // 2. Kamerayı Başlat
            videoSource = new VideoCaptureDevice(videoDevices[cmb_Kameralar.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();

            // 3. Tarayıcıyı (Timer) Başlat (Saniyede 1 kez çalışsın ki bilgisayarı kasmasın)
            timer_Tarayici.Interval = 1000;
            timer_Tarayici.Start();
        }

        // 1. ZİNG İÇİN YENİ EKLENTİ (Sayfanın en üstüne using kısmına ekle, yoksa ekleme)
        // using ZXing.Windows.Compatibility; 

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            // --- YENİ EKLENEN KONTROL: Pencere hazır mı? ---
            if (pic_Kamera.IsHandleCreated)
            {
                pic_Kamera.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Image eskiResim = pic_Kamera.Image;
                    pic_Kamera.Image = frame;
                    if (eskiResim != null)
                    {
                        eskiResim.Dispose();
                    }
                }));
            }
            else
            {
                // Eğer pencere henüz hazır değilse, gelen kareyi çöpe at ki RAM şişmesin
                frame.Dispose();
            }
        }

        private void timer_Tarayici_Tick(object sender, EventArgs e)
        {
            if (pic_Kamera.Image != null)
            {
                try
                {
                    // 1. Z-Xing'i daha akıllı ve zorlayıcı modda başlatıyoruz
                    var reader = new ZXing.Windows.Compatibility.BarcodeReader();

                    // SİHİRLİ AYARLAR BURADA:
                    reader.Options.TryHarder = true; // Bulanık olsa bile çözmek için sınırları zorla!
                    reader.AutoRotate = true;        // Kullanıcı barkodu yan/ters tutarsa resmi beyninde çevirip oku.

                    // 2. Hata vermemesi için o anki görüntünün donmuş bir kopyasını alıyoruz
                    Bitmap anlikGoruntu = new Bitmap(pic_Kamera.Image);

                    // 3. Barkodu ara
                    Result result = reader.Decode(anlikGoruntu);

                    // RAM şişmesin diye kopyayı sil
                    anlikGoruntu.Dispose();


                    if (result != null) // BARKOD BULUNDU!
                    {
                        // Barkod bulununca ekranı anlık yeşil yap (Başarı efekti)
                        using (Graphics g = this.CreateGraphics())
                        {
                            g.Clear(Color.LimeGreen);
                        }
                        System.Threading.Thread.Sleep(100); // 100ms yeşil kalsın
                        timer_Tarayici.Stop();
                        KamerayiKapat();

                        OkunanBarkod = result.Text;
                        System.Media.SystemSounds.Beep.Play();

                        UrunDao dao = new UrunDao();
                        UrunKayitliMi = dao.BarkodMevcutMu(OkunanBarkod);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Okuma sırasında resim atlaması olursa program çökmesin diye yoksayıyoruz
                }
            }
        }

        // --- GÜVENLİK ---
        private void KamerayiKapat()
        {
            // Eğer ortada bir kamera varsa...
            if (videoSource != null)
            {
                // 1. HAMLE: Görüntü kablosunu anında kes! (Yeni kare gelmesini iptal et)
                videoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);

                // 2. HAMLE: Kamera çalışıyorsa dur sinyali gönder
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                }

                // 3. HAMLE: Hafızadan tamamen sil
                videoSource = null;
            }
        }

        private void cmb_Kameralar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Liste boşsa veya henüz yüklenmediyse işlem yapma
            if (videoDevices == null || videoDevices.Count == 0) return;

            // 1. Eski kamerayı acımasızca kapat
            KamerayiKapat();

            // 2. Ekranda eski kameranın son görüntüsü donup kalmasın diye ekranı temizle
            if (pic_Kamera.Image != null)
            {
                pic_Kamera.Image.Dispose();
                pic_Kamera.Image = null;
            }

            // 3. Yeni kamerayı başlat
            videoSource = new VideoCaptureDevice(videoDevices[cmb_Kameralar.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();
        }

        private void BarkodOkuyucuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KamerayiKapat(); // Kullanıcı çarpıdan kapatırsa diye güvenlik kilidi
        }

        private void btn_Kapat_Click(object sender, EventArgs e)
        {
            // 1. Önce kameranın fişini çek (Yoksa arka planda ışığı yanmaya devam eder!)
            KamerayiKapat();

            // 2. Ana Sayfaya (Dashboard) kullanıcının işlemi iptal ettiğini bildir
            this.DialogResult = DialogResult.Cancel;

            // 3. Formu tamamen kapat
            this.Close();
        }
    }
}