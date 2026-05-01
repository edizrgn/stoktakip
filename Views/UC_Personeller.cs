using StokTakip.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StokTakip.Views
{
    public partial class UC_Personeller : UserControl, IPersonelView
    {
        // 1. DAO nesnemiz (Sadece bir kez tanımlanmalı)
        KullaniciDao dao = new KullaniciDao();
        int seciliId;

        // 2. Interface (IPersonelView) için gerekli olan ancak formda kutusu olmayan alanlar
        // Bunları "otomatik property" yaparak interface hatalarını (CS0535) çözüyoruz.
        // --- Interface için gerekli ancak formda kutusu olmayan alanlar ---
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Ad { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Soyad { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Sifre { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Eposta { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Durum { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string KayitTarihi { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object PersonelListesi
        {
            get => guna2DataGridView1.DataSource;
            set => guna2DataGridView1.DataSource = value;
        }

        // 4. Interface Event'leri (Eğer IPersonelView içinde tanımlıysa burada kalmalı)
        public event EventHandler PersonelGuncelle;
        public event EventHandler PersonelBlokla;
        public event EventHandler SeciliPersonelDegisti;

        // 5. Kurucu Metot (Constructor)
        public UC_Personeller()
        {
            InitializeComponent();
            // Form açıldığında verileri otomatik yükle
            PersonelleriYukle();
        }

        // 6. Verileri Veritabanından Çekip Tabloya Basan Metot
        public void PersonelleriYukle()
        {
            try
            {
                // KullaniciDao içindeki metodun adıyla aynı olmalı
                var liste = dao.TumKullanicilariListele();

                if (liste != null)
                {
                    this.PersonelListesi = liste;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        // --- Form Olayları (Event Handlers) ---

        private void UC_Personeller_Load(object sender, EventArgs e)
        {
            // İstersen PersonelleriYukle() metodunu buraya da taşıyabilirsin.
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tabloda bir hücreye tıklandığında yapılacak işlemler
        }

        // Kullanmadığın boş metotları temizleyebilir veya ihtiyacın olduğunda doldurabilirsin.
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string aranan = guna2TextBox1.Text.Trim();

            if (!string.IsNullOrEmpty(aranan))
            {
                // Eğer kutu boş değilse arama yap
                var sonuc = dao.KullaniciAra(aranan);
                this.PersonelListesi = sonuc;
            }
            else
            {
                // Eğer kutu boşsa tüm listeyi tekrar getir
                PersonelleriYukle();
            }
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // Mevcut Id ve Durum değerlerini tablodan çekiyoruz
                int id = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells["Id"].Value);
                int mevcutDurum = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells["Durum"].Value);

                // Mantık: Eğer mevcut durum 1 ise yeni durum 0 olsun, değilse 1 olsun.
                int yeniDurum = (mevcutDurum == 1) ? 0 : 1;

                // Senin önceden yazdığın (ve şimdi doldurduğumuz) metodu çağırıyoruz
                if (dao.DurumGuncelle(id, yeniDurum))
                {
                    MessageBox.Show("Durum başarıyla değiştirildi!");
                    PersonelleriYukle(); // Listeyi yenile ki değişiklik ekrana yansısın
                }
            }
            else
            {
                MessageBox.Show("Lütfen tablodan bir personel seçin!");
            }

        }
    }
}