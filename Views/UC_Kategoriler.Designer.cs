namespace StokTakip
{
    partial class UC_Kategoriler
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            grid_Kategoriler = new Guna.UI2.WinForms.Guna2DataGridView();
            txt_KategoriAdi = new Guna.UI2.WinForms.Guna2TextBox();
            btn_Ekle = new Guna.UI2.WinForms.Guna2Button();
            btn_Sil = new Guna.UI2.WinForms.Guna2Button();
            btn_Guncelle = new Guna.UI2.WinForms.Guna2Button();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)grid_Kategoriler).BeginInit();
            SuspendLayout();
            // 
            // grid_Kategoriler
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            grid_Kategoriler.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            grid_Kategoriler.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            grid_Kategoriler.ColumnHeadersHeight = 45;
            grid_Kategoriler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            grid_Kategoriler.DefaultCellStyle = dataGridViewCellStyle6;
            grid_Kategoriler.GridColor = Color.FromArgb(231, 229, 255);
            grid_Kategoriler.Location = new Point(581, 2);
            grid_Kategoriler.Margin = new Padding(3, 2, 3, 2);
            grid_Kategoriler.Name = "grid_Kategoriler";
            grid_Kategoriler.RowHeadersVisible = false;
            grid_Kategoriler.RowHeadersWidth = 51;
            grid_Kategoriler.RowTemplate.Height = 40;
            grid_Kategoriler.Size = new Size(397, 481);
            grid_Kategoriler.TabIndex = 0;
            grid_Kategoriler.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            grid_Kategoriler.ThemeStyle.AlternatingRowsStyle.Font = null;
            grid_Kategoriler.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            grid_Kategoriler.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            grid_Kategoriler.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            grid_Kategoriler.ThemeStyle.BackColor = Color.White;
            grid_Kategoriler.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            grid_Kategoriler.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            grid_Kategoriler.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            grid_Kategoriler.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            grid_Kategoriler.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            grid_Kategoriler.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            grid_Kategoriler.ThemeStyle.HeaderStyle.Height = 45;
            grid_Kategoriler.ThemeStyle.ReadOnly = false;
            grid_Kategoriler.ThemeStyle.RowsStyle.BackColor = Color.White;
            grid_Kategoriler.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid_Kategoriler.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            grid_Kategoriler.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            grid_Kategoriler.ThemeStyle.RowsStyle.Height = 40;
            grid_Kategoriler.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            grid_Kategoriler.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            grid_Kategoriler.CellClick += grid_Kategoriler_CellClick;
            // 
            // txt_KategoriAdi
            // 
            txt_KategoriAdi.BorderRadius = 8;
            txt_KategoriAdi.CustomizableEdges = customizableEdges9;
            txt_KategoriAdi.DefaultText = "";
            txt_KategoriAdi.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_KategoriAdi.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_KategoriAdi.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_KategoriAdi.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_KategoriAdi.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_KategoriAdi.Font = new Font("Segoe UI", 9F);
            txt_KategoriAdi.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_KategoriAdi.Location = new Point(111, 68);
            txt_KategoriAdi.Name = "txt_KategoriAdi";
            txt_KategoriAdi.PlaceholderText = "Örn: Giyim, Elektronik...";
            txt_KategoriAdi.SelectedText = "";
            txt_KategoriAdi.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txt_KategoriAdi.Size = new Size(250, 45);
            txt_KategoriAdi.TabIndex = 1;
            // 
            // btn_Ekle
            // 
            btn_Ekle.BorderRadius = 8;
            btn_Ekle.CustomizableEdges = customizableEdges11;
            btn_Ekle.DisabledState.BorderColor = Color.DarkGray;
            btn_Ekle.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Ekle.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Ekle.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Ekle.FillColor = Color.MediumSeaGreen;
            btn_Ekle.Font = new Font("Segoe UI", 9F);
            btn_Ekle.ForeColor = Color.White;
            btn_Ekle.Location = new Point(136, 162);
            btn_Ekle.Margin = new Padding(3, 2, 3, 2);
            btn_Ekle.Name = "btn_Ekle";
            btn_Ekle.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btn_Ekle.Size = new Size(197, 42);
            btn_Ekle.TabIndex = 2;
            btn_Ekle.Text = "Ekle";
            btn_Ekle.Click += btn_Ekle_Click;
            // 
            // btn_Sil
            // 
            btn_Sil.BorderRadius = 8;
            btn_Sil.CustomizableEdges = customizableEdges13;
            btn_Sil.DisabledState.BorderColor = Color.DarkGray;
            btn_Sil.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Sil.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Sil.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Sil.FillColor = Color.Crimson;
            btn_Sil.Font = new Font("Segoe UI", 9F);
            btn_Sil.ForeColor = Color.White;
            btn_Sil.Location = new Point(136, 286);
            btn_Sil.Margin = new Padding(3, 2, 3, 2);
            btn_Sil.Name = "btn_Sil";
            btn_Sil.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btn_Sil.Size = new Size(197, 42);
            btn_Sil.TabIndex = 3;
            btn_Sil.Text = "Sil";
            btn_Sil.Click += btn_Sil_Click;
            // 
            // btn_Guncelle
            // 
            btn_Guncelle.BorderRadius = 8;
            btn_Guncelle.CustomizableEdges = customizableEdges15;
            btn_Guncelle.DisabledState.BorderColor = Color.DarkGray;
            btn_Guncelle.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Guncelle.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Guncelle.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Guncelle.FillColor = Color.DodgerBlue;
            btn_Guncelle.Font = new Font("Segoe UI", 9F);
            btn_Guncelle.ForeColor = Color.White;
            btn_Guncelle.Location = new Point(136, 225);
            btn_Guncelle.Margin = new Padding(3, 2, 3, 2);
            btn_Guncelle.Name = "btn_Guncelle";
            btn_Guncelle.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btn_Guncelle.Size = new Size(197, 42);
            btn_Guncelle.TabIndex = 4;
            btn_Guncelle.Text = "Güncelle";
            btn_Guncelle.Click += btn_Guncelle_Click;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(120, 45);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(106, 17);
            guna2HtmlLabel1.TabIndex = 5;
            guna2HtmlLabel1.Text = "Kategori İsimi Girin:";
            guna2HtmlLabel1.Click += guna2HtmlLabel1_Click;
            // 
            // UC_Kategoriler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(btn_Guncelle);
            Controls.Add(btn_Sil);
            Controls.Add(btn_Ekle);
            Controls.Add(txt_KategoriAdi);
            Controls.Add(grid_Kategoriler);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UC_Kategoriler";
            Size = new Size(978, 483);
            ((System.ComponentModel.ISupportInitialize)grid_Kategoriler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DataGridView grid_Kategoriler;
        private Guna.UI2.WinForms.Guna2TextBox txt_KategoriAdi;
        private Guna.UI2.WinForms.Guna2Button btn_Ekle;
        private Guna.UI2.WinForms.Guna2Button btn_Sil;
        private Guna.UI2.WinForms.Guna2Button btn_Guncelle;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}
