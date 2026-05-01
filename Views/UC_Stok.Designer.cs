namespace StokTakip.Views
{
    partial class UC_Stok
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            dataGridView_Stok = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1_barkodNo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            num_Miktar = new Guna.UI2.WinForms.Guna2NumericUpDown();
            txt_BarkodAra = new Guna.UI2.WinForms.Guna2TextBox();
            chk_KritikStok = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            btn_StokDus = new Guna.UI2.WinForms.Guna2Button();
            btn_StokEkle = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Stok).BeginInit();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_Miktar).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_Stok
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridView_Stok.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView_Stok.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView_Stok.ColumnHeadersHeight = 40;
            dataGridView_Stok.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView_Stok.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView_Stok.GridColor = Color.FromArgb(231, 229, 255);
            dataGridView_Stok.Location = new Point(3, -2);
            dataGridView_Stok.Margin = new Padding(3, 2, 3, 2);
            dataGridView_Stok.Name = "dataGridView_Stok";
            dataGridView_Stok.RowHeadersVisible = false;
            dataGridView_Stok.RowHeadersWidth = 51;
            dataGridView_Stok.RowTemplate.Height = 45;
            dataGridView_Stok.Size = new Size(618, 487);
            dataGridView_Stok.TabIndex = 0;
            dataGridView_Stok.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridView_Stok.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridView_Stok.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridView_Stok.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridView_Stok.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridView_Stok.ThemeStyle.BackColor = Color.White;
            dataGridView_Stok.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridView_Stok.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridView_Stok.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView_Stok.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dataGridView_Stok.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridView_Stok.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView_Stok.ThemeStyle.HeaderStyle.Height = 40;
            dataGridView_Stok.ThemeStyle.ReadOnly = false;
            dataGridView_Stok.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridView_Stok.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_Stok.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dataGridView_Stok.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridView_Stok.ThemeStyle.RowsStyle.Height = 45;
            dataGridView_Stok.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridView_Stok.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridView_Stok.CellContentClick += dataGridView_Stok_CellContentClick;
            dataGridView_Stok.SelectionChanged += dataGridView_Stok_SelectionChanged;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.Controls.Add(guna2HtmlLabel2);
            guna2Panel1.Controls.Add(guna2HtmlLabel1);
            guna2Panel1.Controls.Add(guna2HtmlLabel1_barkodNo);
            guna2Panel1.Controls.Add(num_Miktar);
            guna2Panel1.Controls.Add(txt_BarkodAra);
            guna2Panel1.Controls.Add(chk_KritikStok);
            guna2Panel1.Controls.Add(btn_StokDus);
            guna2Panel1.Controls.Add(btn_StokEkle);
            guna2Panel1.CustomizableEdges = customizableEdges11;
            guna2Panel1.FillColor = Color.White;
            guna2Panel1.Location = new Point(623, 2);
            guna2Panel1.Margin = new Padding(3, 2, 3, 2);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel1.ShadowDecoration.Depth = 15;
            guna2Panel1.ShadowDecoration.Enabled = true;
            guna2Panel1.Size = new Size(354, 482);
            guna2Panel1.TabIndex = 1;
            guna2Panel1.Paint += guna2Panel1_Paint;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI Semibold", 10.2F);
            guna2HtmlLabel2.ForeColor = Color.Black;
            guna2HtmlLabel2.Location = new Point(72, 334);
            guna2HtmlLabel2.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(128, 21);
            guna2HtmlLabel2.TabIndex = 11;
            guna2HtmlLabel2.Text = "Kritik Stokları Göster";
            guna2HtmlLabel2.Click += guna2HtmlLabel2_Click;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            guna2HtmlLabel1.ForeColor = Color.Black;
            guna2HtmlLabel1.Location = new Point(72, 106);
            guna2HtmlLabel1.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(39, 21);
            guna2HtmlLabel1.TabIndex = 10;
            guna2HtmlLabel1.Text = "Adet:";
            guna2HtmlLabel1.Click += guna2HtmlLabel1_Click;
            // 
            // guna2HtmlLabel1_barkodNo
            // 
            guna2HtmlLabel1_barkodNo.BackColor = Color.Transparent;
            guna2HtmlLabel1_barkodNo.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            guna2HtmlLabel1_barkodNo.ForeColor = Color.Black;
            guna2HtmlLabel1_barkodNo.Location = new Point(72, 41);
            guna2HtmlLabel1_barkodNo.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel1_barkodNo.Name = "guna2HtmlLabel1_barkodNo";
            guna2HtmlLabel1_barkodNo.Size = new Size(80, 21);
            guna2HtmlLabel1_barkodNo.TabIndex = 7;
            guna2HtmlLabel1_barkodNo.Text = "Barkod No:";
            guna2HtmlLabel1_barkodNo.Click += guna2HtmlLabel1_barkodNo_Click;
            // 
            // num_Miktar
            // 
            num_Miktar.BackColor = Color.Transparent;
            num_Miktar.BorderRadius = 8;
            num_Miktar.CustomizableEdges = customizableEdges1;
            num_Miktar.Font = new Font("Segoe UI", 9F);
            num_Miktar.Location = new Point(72, 132);
            num_Miktar.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            num_Miktar.Name = "num_Miktar";
            num_Miktar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            num_Miktar.Size = new Size(154, 38);
            num_Miktar.TabIndex = 6;
            num_Miktar.ValueChanged += num_Miktar_ValueChanged;
            // 
            // txt_BarkodAra
            // 
            txt_BarkodAra.BorderRadius = 8;
            txt_BarkodAra.CustomizableEdges = customizableEdges3;
            txt_BarkodAra.DefaultText = "";
            txt_BarkodAra.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_BarkodAra.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_BarkodAra.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_BarkodAra.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_BarkodAra.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_BarkodAra.Font = new Font("Segoe UI", 9F);
            txt_BarkodAra.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_BarkodAra.Location = new Point(72, 67);
            txt_BarkodAra.Name = "txt_BarkodAra";
            txt_BarkodAra.PlaceholderText = "";
            txt_BarkodAra.SelectedText = "";
            txt_BarkodAra.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txt_BarkodAra.Size = new Size(154, 26);
            txt_BarkodAra.TabIndex = 5;
            // 
            // chk_KritikStok
            // 
            chk_KritikStok.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            chk_KritikStok.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            chk_KritikStok.CheckedState.InnerBorderColor = Color.White;
            chk_KritikStok.CheckedState.InnerColor = Color.White;
            chk_KritikStok.CustomizableEdges = customizableEdges5;
            chk_KritikStok.Location = new Point(72, 369);
            chk_KritikStok.Margin = new Padding(3, 2, 3, 2);
            chk_KritikStok.Name = "chk_KritikStok";
            chk_KritikStok.ShadowDecoration.CustomizableEdges = customizableEdges6;
            chk_KritikStok.Size = new Size(69, 32);
            chk_KritikStok.TabIndex = 4;
            chk_KritikStok.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            chk_KritikStok.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            chk_KritikStok.UncheckedState.InnerBorderColor = Color.White;
            chk_KritikStok.UncheckedState.InnerColor = Color.White;
            chk_KritikStok.CheckedChanged += chk_KritikStok_CheckedChanged;
            // 
            // btn_StokDus
            // 
            btn_StokDus.BorderRadius = 8;
            btn_StokDus.CustomizableEdges = customizableEdges7;
            btn_StokDus.DisabledState.BorderColor = Color.DarkGray;
            btn_StokDus.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_StokDus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_StokDus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_StokDus.FillColor = Color.FromArgb(220, 53, 69);
            btn_StokDus.Font = new Font("Segoe UI", 9F);
            btn_StokDus.ForeColor = Color.White;
            btn_StokDus.Location = new Point(72, 249);
            btn_StokDus.Margin = new Padding(3, 2, 3, 2);
            btn_StokDus.Name = "btn_StokDus";
            btn_StokDus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btn_StokDus.Size = new Size(167, 45);
            btn_StokDus.TabIndex = 3;
            btn_StokDus.Text = "Stok Düş (-)";
            btn_StokDus.Click += btn_StokDus_Click;
            // 
            // btn_StokEkle
            // 
            btn_StokEkle.BorderRadius = 8;
            btn_StokEkle.CustomizableEdges = customizableEdges9;
            btn_StokEkle.DisabledState.BorderColor = Color.DarkGray;
            btn_StokEkle.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_StokEkle.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_StokEkle.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_StokEkle.FillColor = Color.FromArgb(40, 167, 69);
            btn_StokEkle.Font = new Font("Segoe UI", 9F);
            btn_StokEkle.ForeColor = Color.White;
            btn_StokEkle.Location = new Point(72, 190);
            btn_StokEkle.Margin = new Padding(3, 2, 3, 2);
            btn_StokEkle.Name = "btn_StokEkle";
            btn_StokEkle.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btn_StokEkle.Size = new Size(167, 45);
            btn_StokEkle.TabIndex = 2;
            btn_StokEkle.Text = "Stok Ekle (+)";
            btn_StokEkle.Click += btn_StokEkle_Click;
            // 
            // UC_Stok
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(guna2Panel1);
            Controls.Add(dataGridView_Stok);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UC_Stok";
            Size = new Size(992, 484);
            ((System.ComponentModel.ISupportInitialize)dataGridView_Stok).EndInit();
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_Miktar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DataGridView dataGridView_Stok;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch chk_KritikStok;
        private Guna.UI2.WinForms.Guna2Button btn_StokDus;
        private Guna.UI2.WinForms.Guna2Button btn_StokEkle;
        private Guna.UI2.WinForms.Guna2NumericUpDown num_Miktar;
        private Guna.UI2.WinForms.Guna2TextBox txt_BarkodAra;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1_barkodNo;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
    }
}
