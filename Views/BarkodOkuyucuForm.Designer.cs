namespace StokTakip
{
    partial class BarkodOkuyucuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            cmb_Kameralar = new Guna.UI2.WinForms.Guna2ComboBox();
            pic_Kamera = new PictureBox();
            timer_Tarayici = new System.Windows.Forms.Timer(components);
            btn_Kapat = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pic_Kamera).BeginInit();
            SuspendLayout();
            // 
            // cmb_Kameralar
            // 
            cmb_Kameralar.BackColor = Color.Transparent;
            cmb_Kameralar.CustomizableEdges = customizableEdges1;
            cmb_Kameralar.DrawMode = DrawMode.OwnerDrawFixed;
            cmb_Kameralar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Kameralar.FocusedColor = Color.FromArgb(94, 148, 255);
            cmb_Kameralar.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmb_Kameralar.Font = new Font("Segoe UI", 10F);
            cmb_Kameralar.ForeColor = Color.FromArgb(68, 88, 112);
            cmb_Kameralar.ItemHeight = 30;
            cmb_Kameralar.Location = new Point(1, 2);
            cmb_Kameralar.Name = "cmb_Kameralar";
            cmb_Kameralar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cmb_Kameralar.Size = new Size(265, 36);
            cmb_Kameralar.TabIndex = 0;
            cmb_Kameralar.SelectedIndexChanged += cmb_Kameralar_SelectedIndexChanged;
            // 
            // pic_Kamera
            // 
            pic_Kamera.BackColor = Color.Black;
            pic_Kamera.Location = new Point(1, 44);
            pic_Kamera.Name = "pic_Kamera";
            pic_Kamera.Size = new Size(800, 555);
            pic_Kamera.SizeMode = PictureBoxSizeMode.Zoom;
            pic_Kamera.TabIndex = 2;
            pic_Kamera.TabStop = false;
            // 
            // timer_Tarayici
            // 
            timer_Tarayici.Tick += timer_Tarayici_Tick;
            // 
            // btn_Kapat
            // 
            btn_Kapat.BorderRadius = 8;
            btn_Kapat.CustomizableEdges = customizableEdges3;
            btn_Kapat.DisabledState.BorderColor = Color.DarkGray;
            btn_Kapat.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Kapat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Kapat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Kapat.FillColor = Color.Transparent;
            btn_Kapat.Font = new Font("Segoe UI", 9F);
            btn_Kapat.ForeColor = Color.Black;
            btn_Kapat.Location = new Point(722, 2);
            btn_Kapat.Name = "btn_Kapat";
            btn_Kapat.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_Kapat.Size = new Size(79, 42);
            btn_Kapat.TabIndex = 3;
            btn_Kapat.Text = "X";
            btn_Kapat.Click += btn_Kapat_Click;
            // 
            // BarkodOkuyucuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(btn_Kapat);
            Controls.Add(pic_Kamera);
            Controls.Add(cmb_Kameralar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BarkodOkuyucuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BarkodOkuyucuForm";
            Load += BarkodOkuyucuForm_Load;
            ((System.ComponentModel.ISupportInitialize)pic_Kamera).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ComboBox cmb_Kameralar;
        private PictureBox pic_Kamera;
        private System.Windows.Forms.Timer timer_Tarayici;
        private Guna.UI2.WinForms.Guna2Button btn_Kapat;
    }
}