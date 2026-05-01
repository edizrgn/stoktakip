namespace StokTakip.Views
{
    partial class UC_AnaSayfa
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_AnaSayfa));
            guna2Panel_sonSatislar = new Guna.UI2.WinForms.Guna2Panel();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2Panel_grafik = new Guna.UI2.WinForms.Guna2Panel();
            guna2ShadowPanel4 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            pictureBox_siparisSayisi = new PictureBox();
            label_siparisSayisiAdeti = new Label();
            label8 = new Label();
            guna2ShadowPanel3 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            pictureBox_kritikStok = new PictureBox();
            label_kritikStokAdeti = new Label();
            label_kritikStok = new Label();
            guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            pictureBox_toplamKazanc = new PictureBox();
            label_toplamKazancFiyati = new Label();
            label_toplamKazanc = new Label();
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            pictureBox_toplamSatis = new PictureBox();
            label_toplamSatisFiyati = new Label();
            label_toplamSatis = new Label();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel_sonSatislar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            guna2ShadowPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_siparisSayisi).BeginInit();
            guna2ShadowPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_kritikStok).BeginInit();
            guna2ShadowPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_toplamKazanc).BeginInit();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_toplamSatis).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel_sonSatislar
            // 
            guna2Panel_sonSatislar.BackColor = Color.Transparent;
            guna2Panel_sonSatislar.BorderRadius = 15;
            guna2Panel_sonSatislar.Controls.Add(guna2DataGridView1);
            guna2Panel_sonSatislar.CustomizableEdges = customizableEdges1;
            guna2Panel_sonSatislar.Location = new Point(382, 169);
            guna2Panel_sonSatislar.Margin = new Padding(3, 2, 3, 2);
            guna2Panel_sonSatislar.Name = "guna2Panel_sonSatislar";
            guna2Panel_sonSatislar.Padding = new Padding(9, 8, 9, 8);
            guna2Panel_sonSatislar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel_sonSatislar.ShadowDecoration.Depth = 5;
            guna2Panel_sonSatislar.Size = new Size(573, 336);
            guna2Panel_sonSatislar.TabIndex = 11;
            guna2Panel_sonSatislar.Paint += guna2Panel_sonSatislar_Paint;
            // 
            // guna2DataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 35;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.Dock = DockStyle.Fill;
            guna2DataGridView1.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.Location = new Point(9, 8);
            guna2DataGridView1.Margin = new Padding(3, 2, 3, 2);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 29;
            guna2DataGridView1.Size = new Size(555, 320);
            guna2DataGridView1.TabIndex = 0;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 35;
            guna2DataGridView1.ThemeStyle.ReadOnly = false;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 29;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            // 
            // guna2Panel_grafik
            // 
            guna2Panel_grafik.BackColor = Color.Transparent;
            guna2Panel_grafik.BorderRadius = 15;
            guna2Panel_grafik.CustomizableEdges = customizableEdges3;
            guna2Panel_grafik.Location = new Point(38, 169);
            guna2Panel_grafik.Margin = new Padding(3, 2, 3, 2);
            guna2Panel_grafik.Name = "guna2Panel_grafik";
            guna2Panel_grafik.ShadowDecoration.BorderRadius = 20;
            guna2Panel_grafik.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel_grafik.ShadowDecoration.Depth = 5;
            guna2Panel_grafik.Size = new Size(328, 336);
            guna2Panel_grafik.TabIndex = 10;
            // 
            // guna2ShadowPanel4
            // 
            guna2ShadowPanel4.BackColor = Color.Transparent;
            guna2ShadowPanel4.Controls.Add(pictureBox_siparisSayisi);
            guna2ShadowPanel4.Controls.Add(label_siparisSayisiAdeti);
            guna2ShadowPanel4.Controls.Add(label8);
            guna2ShadowPanel4.FillColor = Color.White;
            guna2ShadowPanel4.Location = new Point(755, 14);
            guna2ShadowPanel4.Margin = new Padding(3, 2, 3, 2);
            guna2ShadowPanel4.Name = "guna2ShadowPanel4";
            guna2ShadowPanel4.Radius = 15;
            guna2ShadowPanel4.ShadowColor = Color.FromArgb(150, 150, 150);
            guna2ShadowPanel4.ShadowDepth = 70;
            guna2ShadowPanel4.ShadowShift = 2;
            guna2ShadowPanel4.Size = new Size(200, 139);
            guna2ShadowPanel4.TabIndex = 9;
            // 
            // pictureBox_siparisSayisi
            // 
            pictureBox_siparisSayisi.Image = (Image)resources.GetObject("pictureBox_siparisSayisi.Image");
            pictureBox_siparisSayisi.Location = new Point(60, 13);
            pictureBox_siparisSayisi.Margin = new Padding(3, 2, 3, 2);
            pictureBox_siparisSayisi.Name = "pictureBox_siparisSayisi";
            pictureBox_siparisSayisi.Size = new Size(60, 43);
            pictureBox_siparisSayisi.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_siparisSayisi.TabIndex = 2;
            pictureBox_siparisSayisi.TabStop = false;
            // 
            // label_siparisSayisiAdeti
            // 
            label_siparisSayisiAdeti.AutoSize = true;
            label_siparisSayisiAdeti.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label_siparisSayisiAdeti.ForeColor = Color.Black;
            label_siparisSayisiAdeti.Location = new Point(46, 84);
            label_siparisSayisiAdeti.Name = "label_siparisSayisiAdeti";
            label_siparisSayisiAdeti.Size = new Size(103, 32);
            label_siparisSayisiAdeti.TabIndex = 1;
            label_siparisSayisiAdeti.Text = "45 Adet";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.ForeColor = SystemColors.WindowFrame;
            label8.Location = new Point(46, 67);
            label8.Name = "label8";
            label8.Size = new Size(74, 19);
            label8.TabIndex = 0;
            label8.Text = "Satış Sayısı";
            label8.Click += label8_Click;
            // 
            // guna2ShadowPanel3
            // 
            guna2ShadowPanel3.BackColor = Color.Transparent;
            guna2ShadowPanel3.Controls.Add(pictureBox_kritikStok);
            guna2ShadowPanel3.Controls.Add(label_kritikStokAdeti);
            guna2ShadowPanel3.Controls.Add(label_kritikStok);
            guna2ShadowPanel3.FillColor = Color.White;
            guna2ShadowPanel3.Location = new Point(517, 14);
            guna2ShadowPanel3.Margin = new Padding(3, 2, 3, 2);
            guna2ShadowPanel3.Name = "guna2ShadowPanel3";
            guna2ShadowPanel3.Radius = 15;
            guna2ShadowPanel3.ShadowColor = Color.FromArgb(150, 150, 150);
            guna2ShadowPanel3.ShadowDepth = 70;
            guna2ShadowPanel3.ShadowShift = 2;
            guna2ShadowPanel3.Size = new Size(200, 139);
            guna2ShadowPanel3.TabIndex = 8;
            // 
            // pictureBox_kritikStok
            // 
            pictureBox_kritikStok.Image = (Image)resources.GetObject("pictureBox_kritikStok.Image");
            pictureBox_kritikStok.Location = new Point(63, 15);
            pictureBox_kritikStok.Margin = new Padding(3, 2, 3, 2);
            pictureBox_kritikStok.Name = "pictureBox_kritikStok";
            pictureBox_kritikStok.Size = new Size(67, 43);
            pictureBox_kritikStok.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_kritikStok.TabIndex = 2;
            pictureBox_kritikStok.TabStop = false;
            // 
            // label_kritikStokAdeti
            // 
            label_kritikStokAdeti.AutoSize = true;
            label_kritikStokAdeti.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label_kritikStokAdeti.ForeColor = Color.Black;
            label_kritikStokAdeti.Location = new Point(52, 84);
            label_kritikStokAdeti.Name = "label_kritikStokAdeti";
            label_kritikStokAdeti.Size = new Size(92, 32);
            label_kritikStokAdeti.TabIndex = 1;
            label_kritikStokAdeti.Text = "8 Ürün";
            // 
            // label_kritikStok
            // 
            label_kritikStok.AutoSize = true;
            label_kritikStok.Font = new Font("Segoe UI", 10F);
            label_kritikStok.ForeColor = SystemColors.WindowFrame;
            label_kritikStok.Location = new Point(63, 67);
            label_kritikStok.Name = "label_kritikStok";
            label_kritikStok.Size = new Size(71, 19);
            label_kritikStok.TabIndex = 0;
            label_kritikStok.Text = "Kritik Stok";
            // 
            // guna2ShadowPanel2
            // 
            guna2ShadowPanel2.BackColor = Color.Transparent;
            guna2ShadowPanel2.Controls.Add(pictureBox_toplamKazanc);
            guna2ShadowPanel2.Controls.Add(label_toplamKazancFiyati);
            guna2ShadowPanel2.Controls.Add(label_toplamKazanc);
            guna2ShadowPanel2.FillColor = Color.White;
            guna2ShadowPanel2.Location = new Point(267, 14);
            guna2ShadowPanel2.Margin = new Padding(3, 2, 3, 2);
            guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            guna2ShadowPanel2.Radius = 15;
            guna2ShadowPanel2.ShadowColor = Color.FromArgb(150, 150, 150);
            guna2ShadowPanel2.ShadowDepth = 70;
            guna2ShadowPanel2.ShadowShift = 2;
            guna2ShadowPanel2.Size = new Size(200, 139);
            guna2ShadowPanel2.TabIndex = 7;
            // 
            // pictureBox_toplamKazanc
            // 
            pictureBox_toplamKazanc.Image = (Image)resources.GetObject("pictureBox_toplamKazanc.Image");
            pictureBox_toplamKazanc.Location = new Point(62, 15);
            pictureBox_toplamKazanc.Margin = new Padding(3, 2, 3, 2);
            pictureBox_toplamKazanc.Name = "pictureBox_toplamKazanc";
            pictureBox_toplamKazanc.Size = new Size(56, 40);
            pictureBox_toplamKazanc.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_toplamKazanc.TabIndex = 2;
            pictureBox_toplamKazanc.TabStop = false;
            // 
            // label_toplamKazancFiyati
            // 
            label_toplamKazancFiyati.AutoSize = true;
            label_toplamKazancFiyati.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label_toplamKazancFiyati.ForeColor = Color.Black;
            label_toplamKazancFiyati.Location = new Point(40, 84);
            label_toplamKazancFiyati.Name = "label_toplamKazancFiyati";
            label_toplamKazancFiyati.Size = new Size(105, 32);
            label_toplamKazancFiyati.TabIndex = 1;
            label_toplamKazancFiyati.Text = "15,400₺";
            // 
            // label_toplamKazanc
            // 
            label_toplamKazanc.AutoSize = true;
            label_toplamKazanc.Font = new Font("Segoe UI", 10F);
            label_toplamKazanc.ForeColor = SystemColors.WindowFrame;
            label_toplamKazanc.Location = new Point(40, 67);
            label_toplamKazanc.Name = "label_toplamKazanc";
            label_toplamKazanc.Size = new Size(99, 19);
            label_toplamKazanc.TabIndex = 0;
            label_toplamKazanc.Text = "Toplam Kazanç";
            // 
            // guna2ShadowPanel1
            // 
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(pictureBox_toplamSatis);
            guna2ShadowPanel1.Controls.Add(label_toplamSatisFiyati);
            guna2ShadowPanel1.Controls.Add(label_toplamSatis);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(27, 14);
            guna2ShadowPanel1.Margin = new Padding(3, 2, 3, 2);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 15;
            guna2ShadowPanel1.ShadowColor = Color.FromArgb(150, 150, 150);
            guna2ShadowPanel1.ShadowDepth = 70;
            guna2ShadowPanel1.ShadowShift = 2;
            guna2ShadowPanel1.Size = new Size(200, 139);
            guna2ShadowPanel1.TabIndex = 6;
            // 
            // pictureBox_toplamSatis
            // 
            pictureBox_toplamSatis.Image = (Image)resources.GetObject("pictureBox_toplamSatis.Image");
            pictureBox_toplamSatis.Location = new Point(64, 10);
            pictureBox_toplamSatis.Margin = new Padding(3, 2, 3, 2);
            pictureBox_toplamSatis.Name = "pictureBox_toplamSatis";
            pictureBox_toplamSatis.Size = new Size(56, 43);
            pictureBox_toplamSatis.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_toplamSatis.TabIndex = 2;
            pictureBox_toplamSatis.TabStop = false;
            // 
            // label_toplamSatisFiyati
            // 
            label_toplamSatisFiyati.AutoSize = true;
            label_toplamSatisFiyati.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label_toplamSatisFiyati.ForeColor = Color.Black;
            label_toplamSatisFiyati.Location = new Point(29, 86);
            label_toplamSatisFiyati.Name = "label_toplamSatisFiyati";
            label_toplamSatisFiyati.Size = new Size(91, 32);
            label_toplamSatisFiyati.TabIndex = 1;
            label_toplamSatisFiyati.Text = "1,250₺";
            // 
            // label_toplamSatis
            // 
            label_toplamSatis.AutoSize = true;
            label_toplamSatis.Font = new Font("Segoe UI", 10F);
            label_toplamSatis.ForeColor = SystemColors.WindowFrame;
            label_toplamSatis.Location = new Point(51, 67);
            label_toplamSatis.Name = "label_toplamSatis";
            label_toplamSatis.Size = new Size(85, 19);
            label_toplamSatis.TabIndex = 0;
            label_toplamSatis.Text = "Toplam Satış";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            guna2HtmlLabel1.Location = new Point(397, 155);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(70, 17);
            guna2HtmlLabel1.TabIndex = 12;
            guna2HtmlLabel1.Text = "Son Satışlar:";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            guna2HtmlLabel2.Location = new Point(56, 155);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(133, 17);
            guna2HtmlLabel2.TabIndex = 0;
            guna2HtmlLabel2.Text = "Ürün Bazlı Stok Grafiği:";
            // 
            // UC_AnaSayfa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 249);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(guna2Panel_sonSatislar);
            Controls.Add(guna2Panel_grafik);
            Controls.Add(guna2ShadowPanel4);
            Controls.Add(guna2ShadowPanel3);
            Controls.Add(guna2ShadowPanel2);
            Controls.Add(guna2ShadowPanel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UC_AnaSayfa";
            Size = new Size(1200, 525);
            Load += UC_AnaSayfa_Load;
            guna2Panel_sonSatislar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            guna2ShadowPanel4.ResumeLayout(false);
            guna2ShadowPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_siparisSayisi).EndInit();
            guna2ShadowPanel3.ResumeLayout(false);
            guna2ShadowPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_kritikStok).EndInit();
            guna2ShadowPanel2.ResumeLayout(false);
            guna2ShadowPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_toplamKazanc).EndInit();
            guna2ShadowPanel1.ResumeLayout(false);
            guna2ShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_toplamSatis).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel_sonSatislar;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_grafik;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel4;
        private PictureBox pictureBox_siparisSayisi;
        private Label label_siparisSayisiAdeti;
        private Label label8;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel3;
        private PictureBox pictureBox_kritikStok;
        private Label label_kritikStokAdeti;
        private Label label_kritikStok;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private PictureBox pictureBox_toplamKazanc;
        private Label label_toplamKazancFiyati;
        private Label label_toplamKazanc;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private PictureBox pictureBox_toplamSatis;
        private Label label_toplamSatisFiyati;
        private Label label_toplamSatis;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
    }
}
